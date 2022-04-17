import { createContext } from "react";
import { TranspilerRule } from "../../api/transpiler-rule";
import { useBuilderReducer } from "./builder.reducer";

interface BuilderContextProps {
  name: string;
  rules: TranspilerRule[];
  addRule: (rule: TranspilerRule) => void;
  removeRule: (ruleName: string) => void;
}

const defaultMethod = () => {
  throw new Error(`BuilderContext Uninitialized`);
};

const defaultBuilderContextProps: BuilderContextProps = {
  name: "",
  rules: [],
  addRule: defaultMethod,
  removeRule: defaultMethod,
};

export const BuilderContext = createContext(defaultBuilderContextProps);

const BuilderContextProvider: React.FC = (props) => {
  const [state, dispatch] = useBuilderReducer();

  const addRule = (rule: TranspilerRule) =>
    dispatch({ type: "ADD_RULE", rule });

  const removeRule = (ruleName: string) =>
    dispatch({ type: "REMOVE_RULE", ruleName });

  return (
    <BuilderContext.Provider
      value={{
        name: state.name,
        rules: state.rules,
        addRule,
        removeRule,
      }}
    >
      {props.children}
    </BuilderContext.Provider>
  );
};

export default BuilderContextProvider;
