import AddRule from "./AddRule";
import EditableLabel from "./EditableLabel";
import RuleList from "./RuleList";
import BuilderContextProvider, {
  BuilderContext,
} from "./BuilderContextProvider";
import { useCallback, useContext } from "react";
import { downloadAsJson } from "./downloadAsJson";

const TranspilerBuilderWithProvider: React.FC = () => {
  return (
    <BuilderContextProvider>
      <TranspilerBuilder></TranspilerBuilder>
    </BuilderContextProvider>
  );
};

const TranspilerBuilder: React.FC = () => {
  const state = useContext(BuilderContext);
  const { name, changeName } = state;

  const onSave = useCallback(() => {
    downloadAsJson(name, state);
  }, [name, state]);

  return (
    <>
      <div className="flex">
        <div className="flex-1 m-5 bg-white shadow overflow-hidden sm:rounded-lg">
          <EditableLabel
            value={name}
            onValueChange={changeName}
            placeholder="Rule List Name"
          />
          <RuleList />
        </div>
        <div className="flex-1 m-5 bg-white shadow overflow-hidden sm:rounded-lg">
          <AddRule />
        </div>
      </div>
      <div className="w-full place-items-center">
        <button
          onClick={onSave}
          className="m-5 max-w-[200px] inline-block px-8 py-3 text-sm font-medium text-white transition bg-indigo-600 rounded hover:scale-110 hover:shadow-xl active:bg-indigo-500 focus:outline-none focus:ring"
        >
          Save Rules
        </button>
      </div>
    </>
  );
};

export default TranspilerBuilderWithProvider;
