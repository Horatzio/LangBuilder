namespace Transpiler
{
    public partial class TranspilerGrammarConcreteVisitor
    {
        public override string VisitConstruct(TranspilerGrammarParser.ConstructContext context)
        {
            return "public class";
        }

        public override string VisitLabel(TranspilerGrammarParser.LabelContext context)
        {
            return context.GetText();
        }

        public override string VisitBlockStart(TranspilerGrammarParser.BlockStartContext context)
        {
            return "{";
        }

        public override string VisitBlockEnd(TranspilerGrammarParser.BlockEndContext context)
        {
            return "}";
        }

        public override string VisitConstructDeclarationBlockStart(TranspilerGrammarParser.ConstructDeclarationBlockStartContext context)
        {
            return Visit(context.construct()) + " " + Visit(context.label()) + " " + Visit(context.blockStart());
        }

        public override string VisitConstructDeclaration(TranspilerGrammarParser.ConstructDeclarationContext context)
        {
            return Visit(context.constructDeclarationBlockStart()) + " " + Visit(context.blockEnd());
        }
    }
}