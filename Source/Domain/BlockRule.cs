namespace LangBuilder.Source.Domain
{
    public class BlockRule : TranspilerRule
    {
        internal readonly TranspilerRule BlockStart;

        internal readonly TranspilerRule BlockBody;

        internal readonly TranspilerRule BlockEnd;

        public BlockRule(TranspilerRule blockStart, TranspilerRule blockBody, TranspilerRule blockEnd)
        {
            BlockStart = blockStart;
            BlockBody = blockBody;
            BlockEnd = blockEnd;
        }

        public override string GrammarRule => $"{BlockStart.Name} {BlockBody.Name} {BlockEnd.Name}";
        public override string RuleBody => $"return context.{BlockBody}() + context.{BlockBody.Name}() + context.{BlockEnd}() ;";
    }
}
