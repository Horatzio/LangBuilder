grammar Testgrammar;

options {
	language = 'CSharp';
}

program: statement+ EOF;

statement: rule1 | rule2;

sequence: rule1 rule2 rule1;

rule1: 'hello';
rule2: 'AB';
