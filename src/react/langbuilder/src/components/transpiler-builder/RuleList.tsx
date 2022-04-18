import { RadioGroup } from "@headlessui/react";
import { useContext, useState } from "react";
import { TranspilerRule } from "../../api/transpiler-rule";
import { BuilderContext } from "./BuilderContextProvider";
import { BsFillInboxFill } from "react-icons/bs";

interface RuleListItemProps {
  rule: TranspilerRule;
}

const RuleListItem: React.FC<RuleListItemProps> = ({ rule }) => {
  return (
    <>
      <RadioGroup.Option
        key={rule.name}
        value={rule}
        className={({ active, checked }) =>
          `${
            active
              ? "ring-2 ring-offset-2 ring-offset-sky-300 ring-white ring-opacity-60"
              : ""
          }
                  ${
                    checked ? "bg-sky-900 bg-opacity-75 text-white" : "bg-white"
                  }
                    relative rounded-lg shadow-md px-5 py-4 cursor-pointer flex focus:outline-none`
        }
      >
        {rule.name}
      </RadioGroup.Option>
    </>
  );
};

const RuleList: React.FC = () => {
  const { rules } = useContext(BuilderContext);
  const [selected, setSelected] = useState(rules[0] || null);

  return (
    <>
      <div className="w-full px-4 py-16">
        <div className="w-full max-w-md mx-auto">
          {rules.length > 0 ? (
            <RadioGroup value={selected} onChange={setSelected}>
              <div className="space-y-2">
                {rules.map((rule) => (
                  <RuleListItem rule={rule} />
                ))}
              </div>
            </RadioGroup>
          ) : (
            <div className="flex flex-col place-items-center align-center">
              <BsFillInboxFill />
              <p>No rules</p>
            </div>
          )}
        </div>
      </div>
    </>
  );
};

export default RuleList;
