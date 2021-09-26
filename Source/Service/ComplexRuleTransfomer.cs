using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LangBuilder.Source.Domain;

namespace LangBuilder.Source.Service
{
    public class ComplexRuleTransfomer
    {
        public ISet<TranspilerRuleViewModel> Models { get; set; }
        public IList<TranspilerRule> Rules { get; set; }
        public static IEnumerable<TranspilerRule> TransformComplexRules(IEnumerable<TranspilerRuleViewModel> models, IEnumerable<TranspilerRule> simpleRules)
        {
            var instance = new ComplexRuleTransfomer
            {
                Models = new HashSet<TranspilerRuleViewModel>(models),
                Rules = new List<TranspilerRule>(simpleRules),
            };

            return instance.TransformComplexRules();
        }

        private IEnumerable<TranspilerRule> TransformComplexRules()
        {
            while (Models.Any())
            {
                var model = Models.Last();

                if(!Rules.Select(r => r.Name).Contains(model.Name)){}
                {
                    TransformComplexRule(model);
                }

                Models.Remove(model);
            }

            return Rules;
        }

        private void TransformComplexRule(TranspilerRuleViewModel model)
        {
            var dependencies = GetComplexRuleDependencies(model);

            dependencies = dependencies.Where(d => !Rules.Select(r => r.Name).Contains(d));

            var dependenciesQueue = new LinkedList<string>();

            dependenciesQueue.AddFirst(model.Name);

            foreach (var dependency in dependencies)
            {
                dependenciesQueue.AddFirst(dependency);
            }

            while (dependenciesQueue.Any())
            {
                var firstDependency = dependenciesQueue.First.Value;
                dependenciesQueue.RemoveFirst();

                var dependencyModel = Models.First(m => m.Name == firstDependency);

                dependencies = GetComplexRuleDependencies(dependencyModel);
                dependencies = dependencies.Where(d => !Rules.Select(r => r.Name).Contains(d));

                var dependenciesList = dependencies.ToList();

                if (!dependenciesList.Any() )
                {
                    if (!Rules.Select(r => r.Name).Contains(dependencyModel.Name))
                    {
                        var rule = TransformComplexRuleWithResolvedDependencies(dependencyModel);
                        Rules.Add(rule);
                    }
                }
                else
                {
                    dependenciesQueue.AddFirst(firstDependency);
                    foreach (var dependency in dependenciesList)
                    {
                        if (dependenciesQueue.Contains(dependency))
                        {
                            throw new ApplicationException("Cyclic complex rule dependencies");
                        }
                        dependenciesQueue.AddFirst(dependency);
                    }
                }
            }
        }

        private IEnumerable<string> GetComplexRuleDependencies(TranspilerRuleViewModel model)
        {
            return model switch
            {
                BlockRuleViewModel viewModel => new []{ viewModel.BlockStart, viewModel.BlockBody, viewModel.BlockEnd },
                RuleSequenceRuleViewModel viewModel => viewModel.Rules,
                _ => throw new ApplicationException("Undefined complex rule")
            };
        }

        private TranspilerRule TransformComplexRuleWithResolvedDependencies(TranspilerRuleViewModel model)
        {
            return model switch
            {
                BlockRuleViewModel viewModel => new BlockRule
                {
                    Name = model.Name,
                    BlockStart = Rules.First(r => r.Name == viewModel.BlockStart),
                    BlockBody = Rules.First(r => r.Name == viewModel.BlockBody),
                    BlockEnd = Rules.First(r => r.Name == viewModel.BlockEnd)
                },
                RuleSequenceRuleViewModel viewModel => new RuleSequenceRule
                {
                    Name = model.Name,
                    Delimiter = viewModel.Delimiter,
                    Rules = Rules.Where(r => viewModel.Rules.Contains(r.Name))
                        .ToArray()
                },
                _ => throw new ApplicationException("Undefined complex rule")
            };
        }
    }
}
