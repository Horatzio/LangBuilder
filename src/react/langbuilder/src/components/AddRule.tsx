import { useCallback, useContext, useState } from "react";
import { RuleType } from "../api/rule-type";
import { BuilderContext } from "./transpiler-builder/BuilderContextProvider";

const AddRule: React.FC = () => {
  const { addRule } = useContext(BuilderContext);

  const [name, setName] = useState("");
  const [ruleType, setRuleType] = useState(RuleType.DirectTranslation);
  const [isStatement, setIsStatement] = useState(false);

  const onSubmit = useCallback(() => {
    addRule({
      name,
      type: ruleType,
      isStatement,
    });
  }, [name, ruleType, isStatement]);

  return (
    <>
      <label>Rule Name</label>
      <input value={name} onChange={(e) => setName(e.target.value)}></input>
      <label>Rule Type</label>
      <select
        value={RuleType[ruleType]}
        onChange={(e) => setRuleType(RuleType[e.target.value as RuleType])}
      >
        {Object.keys(RuleType)
          .map((k) => RuleType[k as RuleType] as string)
          .map((t) => (
            <option key={t} value={t}>
              {t}
            </option>
          ))}
      </select>
      <label>Is Statement</label>
      <input
        type="checkbox"
        checked={isStatement}
        onChange={(e) => setIsStatement(e.target.checked)}
      ></input>
      <button onClick={onSubmit}>Add Rule</button>
    </>
  );
};

export default AddRule;
