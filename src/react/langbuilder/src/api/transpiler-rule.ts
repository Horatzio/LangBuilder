import {
  RuleType,
  DirectTranslation,
  Expression,
  RuleOptionSequence,
  RuleSequence,
} from "./rule-type";

export interface TranspilerRule {
  type: RuleType;
  name: string;
  isStatement: boolean;
}

export interface DirectTranslationRule extends TranspilerRule {
  type: DirectTranslation;
  inputSymbol: string;
  outputSymbol: string;
}

export interface ExpressionRule extends TranspilerRule {
  type: Expression;
  expression: string;
}

export const LabelRule: ExpressionRule = {
  expression: "Label",
  name: "label",
  isStatement: false,
  type: "Expression",
};

export interface RuleOptionSequenceRule extends TranspilerRule {
  type: RuleOptionSequence;
  rules: TranspilerRule[];
}

export interface RuleSequenceRule extends TranspilerRule {
  type: RuleSequence;
  delimiter: string;
  rules: TranspilerRule[];
}
