import AddRule from "./AddRule";
import RuleList from "./RuleList";
import BuilderContextProvider from "./transpiler-builder/BuilderContextProvider";

const TranspilerBuilder: React.FC = () => {
  return (
    <BuilderContextProvider>
      <div className="flex">
        <div className="flex-1 bg-white shadow overflow-hidden sm:rounded-lg">
          <RuleList />
        </div>
        <div className="flex-1 bg-white shadow overflow-hidden sm:rounded-lg">
          <AddRule />
        </div>
      </div>
    </BuilderContextProvider>
  );
};

export default TranspilerBuilder;
