import { useContext } from "react";
import { BuilderContext } from "./transpiler-builder/BuilderContextProvider";

const RuleList: React.FC = () => {
  const { rules } = useContext(BuilderContext);

  return (
    <>
      {rules.map((r) => (
        <>
          <h1>{r.name}</h1>
          <sub>{r.type}</sub>
        </>
      ))}
    </>
  );
};

export default RuleList;
