import { RuleType } from "./rule-type";

export interface TranspilerRule {
  type: RuleType;
  name: string;
  isStatement: boolean;
}

export interface DirectTranslationRule extends TranspilerRule {
  type: RuleType.DirectTranslation;
  inputSymbol: string;
  outputSymbol: string;
}

export interface ExpressionRule extends TranspilerRule {
  type: RuleType.Expression;
  expression: string;
}

export const LabelRule: ExpressionRule = {
  expression: "Label",
  name: "label",
  isStatement: false,
  type: RuleType.Expression,
};

export interface RuleOptionSequenceRule extends TranspilerRule {
  type: RuleType.RuleOptionSequence;
  rules: TranspilerRule[];
}

export interface RuleSequenceRule extends TranspilerRule {
  type: RuleType.RuleSequence;
  delimiter: string;
  rules: TranspilerRule[];
}
