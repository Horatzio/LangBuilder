export default {};

type RuleList = {
  name: string;
  rules: Rule[];
};

type Rule = {
  name: string;
  isStatement: boolean;
} & (
  | DirectTranslationRule
  | ExpressionRule
  | RuleOptionSequenceRule
  | RuleSequenceRule
);

type DirectTranslationRule = {
  ruleType: "DirectTranslation";
  inputSymbol: string;
  outputSymbol: string;
};

type ExpressionRule = {
  ruleType: "Expression";
  expression: string;
};

type RuleOptionSequenceRule = {
  ruleType: "RuleOptionSequenceRule";
  ruleNames: string[];
};
type RuleSequenceRule = {
  ruleType: "RuleSequenceRule";
  ruleNames: string[];
};
