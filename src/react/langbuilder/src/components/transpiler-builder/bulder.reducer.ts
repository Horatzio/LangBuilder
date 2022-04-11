import { useReducer } from "react";
import { TranspilerRule } from "../../api/transpiler-rule";

interface State {
  name: string;
  rules: TranspilerRule[];
}

type Actions =
  | {
      type: "ADD_RULE";
      rule: TranspilerRule;
    }
  | {
      type: "REMOVE_RULE";
      ruleName: string;
    };

const reducer = (state: State, action: Actions) => {
  switch (action.type) {
    case "ADD_RULE": {
      return {
        ...state,
        rules: [...state.rules, action.rule],
      };
    }
    case "REMOVE_RULE": {
      return {
        ...state,
        rules: [...state.rules.filter((r) => r.name !== action.ruleName)],
      };
    }
    default:
      return state;
  }
};

const initialState: State = {
  name: "",
  rules: [],
};

export const useBuilderReducer = () => useReducer(reducer, initialState);
