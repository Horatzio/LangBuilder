import { TranspilerRule } from "./transpiler-rule";

export interface TranspilerModel {
  name: string;
  rules: TranspilerRule[];
}
