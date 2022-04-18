import { createContext } from "react";
import { TranspilerRule } from "../../api/transpiler-rule";
import { useBuilderReducer } from "./builder.reducer";

interface BuilderContextProps {
  name: string;
  rules: TranspilerRule[];
  addRule: (rule: TranspilerRule) => void;
  removeRule: (ruleName: string) => void;
  changeName: (newName: string) => void;
}

const defaultMethod = () => {
  throw new Error(`BuilderContext Uninitialized`);
};

const defaultBuilderContextProps: BuilderContextProps = {
  name: "",
  rules: [],
  addRule: defaultMethod,
  removeRule: defaultMethod,
  changeName: defaultMethod,
};

export const BuilderContext = createContext(defaultBuilderContextProps);

const BuilderContextProvider: React.FC = (props) => {
  const [state, dispatch] = useBuilderReducer();

  return (
    <BuilderContext.Provider
      value={{
        name: state.name,
        rules: state.rules,
        addRule: (rule: TranspilerRule) => dispatch({ type: "ADD_RULE", rule }),
        removeRule: (ruleName: string) =>
          dispatch({ type: "REMOVE_RULE", ruleName }),
        changeName: (newName: string) =>
          dispatch({ type: "CHANGE_NAME", newName }),
      }}
    >
      {props.children}
    </BuilderContext.Provider>
  );
};

export default BuilderContextProvider;
