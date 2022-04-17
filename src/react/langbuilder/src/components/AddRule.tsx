import { Menu, Transition } from "@headlessui/react";
import { Fragment, useCallback, useContext, useState } from "react";
import { RuleType } from "../api/rule-type";
import { BuilderContext } from "./transpiler-builder/BuilderContextProvider";
import { HiSelector } from "react-icons/hi";

interface RuleTypeSelectorProps {
  ruleType: RuleType;
  setRuleType: React.Dispatch<React.SetStateAction<RuleType>>;
}

const RuleTypeSelector: React.FC<RuleTypeSelectorProps> = ({
  ruleType,
  setRuleType,
}) => {
  return (
    <>
      <div>
        <Menu as="div" className="relative inline-block text-left">
          <div>
            <Menu.Button className="inline-flex justify-center w-full rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-100 focus:ring-indigo-500">
              {ruleType}
              <HiSelector className="-mr-1 ml-2 h-5 w-5" aria-hidden="true" />
            </Menu.Button>
          </div>
          <Transition
            as={Fragment}
            enter="transition ease-out duration-100"
            enterFrom="transform opacity-0 scale-95"
            enterTo="transform opacity-100 scale-100"
            leave="transition ease-in duration-75"
            leaveFrom="transform opacity-100 scale-100"
            leaveTo="transform opacity-0 scale-95"
          >
            <Menu.Items className="fixed w-56 mt-2 origin-top-right bg-white divide-y divide-gray-100 rounded-md shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none">
              <div className="px-1 py-1">
                {Object.keys(RuleType)
                  .map((k) => ({
                    key: k as RuleType,
                    val: RuleType[k as RuleType] as string,
                  }))
                  .map(({ key, val }) => (
                    <Menu.Item
                      as="div"
                      className="cursor-pointer"
                      key={key}
                      onClick={() => setRuleType(RuleType[key])}
                    >
                      {({ active }) => {
                        return (
                          <div className={active ? "text-sky-400" : ""}>
                            {val}
                          </div>
                        );
                      }}
                    </Menu.Item>
                  ))}
              </div>
            </Menu.Items>
          </Transition>
        </Menu>
      </div>
    </>
  );
};

const AddRule: React.FC = () => {
  const { addRule } = useContext(BuilderContext);

  const [name, setName] = useState("");
  const [ruleType, setRuleType] = useState(Object.values(RuleType)[0]);
  const [isStatement, setIsStatement] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");

  const onSubmit = useCallback(() => {
    if (!name) {
      setErrorMessage("Name must be set!");
      return;
    }

    addRule({
      name,
      type: ruleType,
      isStatement,
    });
  }, [addRule, name, ruleType, isStatement]);

  return (
    <>
      <div className="flex flex-col">
        <input
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder={"Rule Name"}
        />
        <RuleTypeSelector
          ruleType={ruleType}
          setRuleType={setRuleType}
        ></RuleTypeSelector>
        <label>Is Statement</label>
        <input
          type="checkbox"
          checked={isStatement}
          onChange={(e) => setIsStatement(e.target.checked)}
        ></input>
        <button onClick={onSubmit}>Add Rule</button>
        {errorMessage && <div className="text-rose-400">{errorMessage}</div>}
      </div>
    </>
  );
};

export default AddRule;
