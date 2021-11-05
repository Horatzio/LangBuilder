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

        public override string VisitBlockStartSeparator(TranspilerGrammarParser.BlockStartSeparatorContext context)
        {
            return context.GetText();
        }

        public override string VisitAnything(TranspilerGrammarParser.AnythingContext context)
        {
            return context.GetText();
        }

        public override string VisitConstructDeclarationBlockStart(TranspilerGrammarParser.ConstructDeclarationBlockStartContext context)
        {
            return context.construct() + context.label() + context.blockStart() + context.blockStartSeparator()}

        public override string VisitConstructDeclaration(TranspilerGrammarParser.ConstructDeclarationContext context)
        {
            return context.anything() + context.anything() + context.blockEnd();
        }
    }
}