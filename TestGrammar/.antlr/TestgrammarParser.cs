//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from c:\Studythings\LangBuilder\LangBuilderCore\TestGrammar\Testgrammar.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public partial class TestgrammarParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2;
	public const int
		RULE_program = 0, RULE_statement = 1, RULE_sequence = 2, RULE_rule1 = 3, 
		RULE_rule2 = 4;
	public static readonly string[] ruleNames = {
		"program", "statement", "sequence", "rule1", "rule2"
	};

	private static readonly string[] _LiteralNames = {
		null, "'hello'", "'AB'"
	};
	private static readonly string[] _SymbolicNames = {
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Testgrammar.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static TestgrammarParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public TestgrammarParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public TestgrammarParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class ProgramContext : ParserRuleContext {
		public ITerminalNode Eof() { return GetToken(TestgrammarParser.Eof, 0); }
		public StatementContext[] statement() {
			return GetRuleContexts<StatementContext>();
		}
		public StatementContext statement(int i) {
			return GetRuleContext<StatementContext>(i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_program; } }
	}

	[RuleVersion(0)]
	public ProgramContext program() {
		ProgramContext _localctx = new ProgramContext(Context, State);
		EnterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 11;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 10; statement();
				}
				}
				State = 13;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( _la==T__0 || _la==T__1 );
			State = 15; Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class StatementContext : ParserRuleContext {
		public Rule1Context rule1() {
			return GetRuleContext<Rule1Context>(0);
		}
		public Rule2Context rule2() {
			return GetRuleContext<Rule2Context>(0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_statement; } }
	}

	[RuleVersion(0)]
	public StatementContext statement() {
		StatementContext _localctx = new StatementContext(Context, State);
		EnterRule(_localctx, 2, RULE_statement);
		try {
			State = 19;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case T__0:
				EnterOuterAlt(_localctx, 1);
				{
				State = 17; rule1();
				}
				break;
			case T__1:
				EnterOuterAlt(_localctx, 2);
				{
				State = 18; rule2();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class SequenceContext : ParserRuleContext {
		public Rule1Context[] rule1() {
			return GetRuleContexts<Rule1Context>();
		}
		public Rule1Context rule1(int i) {
			return GetRuleContext<Rule1Context>(i);
		}
		public Rule2Context rule2() {
			return GetRuleContext<Rule2Context>(0);
		}
		public SequenceContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_sequence; } }
	}

	[RuleVersion(0)]
	public SequenceContext sequence() {
		SequenceContext _localctx = new SequenceContext(Context, State);
		EnterRule(_localctx, 4, RULE_sequence);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 21; rule1();
			State = 22; rule2();
			State = 23; rule1();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Rule1Context : ParserRuleContext {
		public Rule1Context(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_rule1; } }
	}

	[RuleVersion(0)]
	public Rule1Context rule1() {
		Rule1Context _localctx = new Rule1Context(Context, State);
		EnterRule(_localctx, 6, RULE_rule1);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 25; Match(T__0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Rule2Context : ParserRuleContext {
		public Rule2Context(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_rule2; } }
	}

	[RuleVersion(0)]
	public Rule2Context rule2() {
		Rule2Context _localctx = new Rule2Context(Context, State);
		EnterRule(_localctx, 8, RULE_rule2);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 27; Match(T__1);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x3', '\x4', ' ', '\x4', '\x2', '\t', '\x2', '\x4', '\x3', 
		'\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', '\x5', '\x4', 
		'\x6', '\t', '\x6', '\x3', '\x2', '\x6', '\x2', '\xE', '\n', '\x2', '\r', 
		'\x2', '\xE', '\x2', '\xF', '\x3', '\x2', '\x3', '\x2', '\x3', '\x3', 
		'\x3', '\x3', '\x5', '\x3', '\x16', '\n', '\x3', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x5', '\x3', '\x5', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x2', '\x2', '\a', '\x2', '\x4', '\x6', 
		'\b', '\n', '\x2', '\x2', '\x2', '\x1C', '\x2', '\r', '\x3', '\x2', '\x2', 
		'\x2', '\x4', '\x15', '\x3', '\x2', '\x2', '\x2', '\x6', '\x17', '\x3', 
		'\x2', '\x2', '\x2', '\b', '\x1B', '\x3', '\x2', '\x2', '\x2', '\n', '\x1D', 
		'\x3', '\x2', '\x2', '\x2', '\f', '\xE', '\x5', '\x4', '\x3', '\x2', '\r', 
		'\f', '\x3', '\x2', '\x2', '\x2', '\xE', '\xF', '\x3', '\x2', '\x2', '\x2', 
		'\xF', '\r', '\x3', '\x2', '\x2', '\x2', '\xF', '\x10', '\x3', '\x2', 
		'\x2', '\x2', '\x10', '\x11', '\x3', '\x2', '\x2', '\x2', '\x11', '\x12', 
		'\a', '\x2', '\x2', '\x3', '\x12', '\x3', '\x3', '\x2', '\x2', '\x2', 
		'\x13', '\x16', '\x5', '\b', '\x5', '\x2', '\x14', '\x16', '\x5', '\n', 
		'\x6', '\x2', '\x15', '\x13', '\x3', '\x2', '\x2', '\x2', '\x15', '\x14', 
		'\x3', '\x2', '\x2', '\x2', '\x16', '\x5', '\x3', '\x2', '\x2', '\x2', 
		'\x17', '\x18', '\x5', '\b', '\x5', '\x2', '\x18', '\x19', '\x5', '\n', 
		'\x6', '\x2', '\x19', '\x1A', '\x5', '\b', '\x5', '\x2', '\x1A', '\a', 
		'\x3', '\x2', '\x2', '\x2', '\x1B', '\x1C', '\a', '\x3', '\x2', '\x2', 
		'\x1C', '\t', '\x3', '\x2', '\x2', '\x2', '\x1D', '\x1E', '\a', '\x4', 
		'\x2', '\x2', '\x1E', '\v', '\x3', '\x2', '\x2', '\x2', '\x4', '\xF', 
		'\x15',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
