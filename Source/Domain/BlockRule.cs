namespace LangBuilder.Source.Domain
{
    public class BlockRule : TranspilerRule
    {
        private readonly TranspilerRule _blockStart;

        private readonly TranspilerRule _blockBody;

        private readonly TranspilerRule _blockEnd;

        public BlockRule(TranspilerRule blockStart, TranspilerRule blockBody, TranspilerRule blockEnd)
        {
            _blockStart = blockStart;
            _blockBody = blockBody;
            _blockEnd = blockEnd;
        }

        public override string GrammarRule => $"{_blockStart.Name} {_blockBody.Name} {_blockEnd.Name}";
        public override string RuleBody => $"return context.{_blockBody}() + context.{_blockBody.Name}() + context.{_blockEnd}() ;";
    }
}
