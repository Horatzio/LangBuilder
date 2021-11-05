grammar TranspilerGrammar;

options {
    language = 'CSharp';
}

program: statement+ EOF;

statement: Construct | Label | BlockStart | BlockEnd | BlockStartSeparator | Anything | ConstructDeclarationBlockStart | ConstructDeclaration;

Construct: 'construct';
Label: '[a-zA-Z0-9_]+';
BlockStart: '[';
BlockEnd: ']';
BlockStartSeparator: '(' ' | '
')*';
Anything: '.+';
ConstructDeclarationBlockStart: Construct Label BlockStart BlockStartSeparator;
ConstructDeclaration: ConstructDeclarationBlockStart Anything BlockEnd;
