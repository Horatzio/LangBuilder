import BuilderContextProvider from "./transpiler-builder/BuilderContextProvider";
import RuleList from "./RuleList";
import AddRule from "./AddRule";

const TranspilerBuilder: React.FC = () => {
  return (
    <BuilderContextProvider>
      <RuleList />
      <AddRule></AddRule>
    </BuilderContextProvider>
  );
};

export default TranspilerBuilder;
