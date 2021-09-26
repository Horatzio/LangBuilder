using LangBuilder.Source.Extensions;

namespace LangBuilder.Source.Domain
{
    public class BlockRuleViewModel : TranspilerRuleViewModel
    {
        public string BlockStart { get; set; }
        public string BlockBody { get; set; }
        public string BlockEnd { get; set; }
    }

    public class BlockRule : TranspilerRule
    {
        public TranspilerRule BlockStart { get; set; }
        public TranspilerRule BlockBody { get; set; }
        public TranspilerRule BlockEnd { get; set; }

        public override string GrammarRule => $"{BlockStart.Name} {BlockBody.Name} {BlockEnd.Name}";
        public override string RuleBody => $"return context.{BlockBody.Name.ToLowercaseFirstLetter()}() + context.{BlockBody.Name.ToLowercaseFirstLetter()}() + context.{BlockEnd.Name.ToLowercaseFirstLetter()}() ;";
    }
}
