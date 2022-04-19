grammar TranspilerGrammar;

options {
    language = 'CSharp';
}

Newline: ('\r' '\n'? | '\n') -> skip;
Whitespace: [ \t]+ -> skip;
Label: [a-zA-Z0-9]+;

program: statement+ EOF;

statement: cattopotat;

cattopotat: 'cat';

