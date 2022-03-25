grammar TranspilerGrammar;

options {
    language = 'CSharp';
}

Newline: ('\r' '\n'? | '\n') -> skip;
Whitespace: [ \t]+ -> skip;
Label: [a-zA-Z0-9]+;

program: statement+ EOF;

statement: constructDeclaration;

construct: 'construct';

label: Label;

blockStart: '[';

blockEnd: ']';

constructDeclarationBlockStart: construct label blockStart;

constructDeclaration: constructDeclarationBlockStart blockEnd;

