using System.Linq;

namespace Transpiler
{
    public partial class TranspilerGrammarConcreteVisitor : TranspilerGrammarBaseVisitor<string>
    {
        public override string VisitProgram(TranspilerGrammarParser.ProgramContext context)
        {
            var result = context.statement().ToList().Select(VisitStatement).Aggregate((s1, s2) =>
            {
                s1 += s2;
                return s1;
            });
            return result;
        }
    }
}