grammar {{ model.grammar_name }};

options {
    language = 'CSharp';
}

Newline: ('\r' '\n'? | '\n') -> skip;
Whitespace: [ \t]+ -> skip;
Label: [a-zA-Z0-9]+;

program: statement+ EOF;
{{ isStatement(x) = x.is_statement == true }}
statement: {{ model.rules | array.filter @isStatement | array.map "name" | array.join " | " }};
{{ for rule in model.rules }}
{{ rule.name }}: {{ rule.grammar_rule }};
{{ end }}
