import { Menu, Switch, Transition } from "@headlessui/react";
import { Fragment, useCallback, useContext, useState } from "react";
import { RuleType } from "../../api/rule-type";
import { BuilderContext } from "./BuilderContextProvider";
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
            <Menu.Items className="z-10 fixed w-56 mt-2 origin-top-right bg-white divide-y divide-gray-100 rounded-md shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none">
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

interface ExtraRuleFieldsProps {
  ruleType: RuleType;
  getField: (k: string, def: any) => any;
  setField: (k: string, v: any) => void;
}

const ExtraRuleFields: React.FC<ExtraRuleFieldsProps> = ({
  ruleType,
  getField,
  setField,
}) => {
  function switchOnRuleType(ruleType: RuleType) {
    switch (ruleType) {
      case RuleType.DirectTranslation:
        return DirectTranslation();
      case RuleType.Expression:
        return <></>;
      case RuleType.RuleOptionSequence:
        return <></>;
      case RuleType.RuleSequence:
        return <></>;
    }
  }

  function DirectTranslation() {
    const [inputSymbol, setInputSymbol] = [
      getField("inputSymbol", ""),
      (newValue: any) => setField("inputSymbol", newValue),
    ];
    const [outputSymbol, setOutputSymbol] = [
      getField("outputSymbol", ""),
      (newValue: any) => setField("outputSymbol", newValue),
    ];

    return (
      <>
        <label
          className="relative block m-4 p-3 border-2 border-gray-200 rounded-lg"
          htmlFor="inputSymbol"
        >
          <span className="text-xs font-medium text-gray-500">
            Input Symbol
          </span>
          <input
            className="w-full m-1 p-1 text-sm border-4 bg-gray-200 rounded-xs focus:outline-none font-['Consolas']"
            id="name"
            type="text"
            placeholder="number"
            value={inputSymbol}
            onChange={(e) => setInputSymbol(e.target.value)}
            autoComplete="off"
          />
        </label>
        <label
          className="relative block m-4 p-3 border-2 border-gray-200 rounded-lg"
          htmlFor="inputSymbol"
        >
          <span className="text-xs font-medium text-gray-500">
            Output Symbol
          </span>
          <input
            className="w-full m-1 p-1 text-sm border-4 bg-gray-200 rounded-xs focus:outline-none font-['Consolas']"
            id="name"
            type="text"
            placeholder="int"
            value={outputSymbol}
            onChange={(e) => setOutputSymbol(e.target.value)}
            autoComplete="off"
          />
        </label>
      </>
    );
  }

  return <>{switchOnRuleType(ruleType)}</>;
};

const AddRule: React.FC = () => {
  const { addRule } = useContext(BuilderContext);

  const [name, setName] = useState("");
  const [ruleType, setRuleType] = useState(Object.values(RuleType)[0]);
  const [isStatement, setIsStatement] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const [ruleExtraFields, setRuleExtraFields] = useState(
    {} as Record<string, any>
  );

  const onSubmit = useCallback(() => {
    if (!name) {
      setErrorMessage("Name must be set!");
      return;
    }

    addRule({
      name,
      type: ruleType,
      isStatement,
      ...ruleExtraFields,
    });
  }, [name, addRule, ruleType, isStatement, ruleExtraFields]);

  return (
    <>
      <div className="flex flex-col">
        <label
          className="relative block m-4 p-3 border-2 border-gray-200 rounded-lg"
          htmlFor="name"
        >
          <span className="text-xs font-medium text-gray-500">Rule Name</span>

          <input
            className="w-full p-2 text-sm border-none focus:ring-0"
            id="name"
            type="text"
            placeholder="cool-rule-1"
            value={name}
            onChange={(e) => setName(e.target.value)}
            autoComplete="off"
          />
        </label>
        <label
          className="flex flex-row relative block m-4 p-3 border-2 border-gray-200 rounded-lg"
          htmlFor="name"
        >
          <div className="p-1 place-content-center stext-xs font-medium text-gray-500 mr-5">
            Is Statement
          </div>

          <Switch
            checked={isStatement}
            onChange={setIsStatement}
            className={`${isStatement ? "bg-teal-700" : "bg-teal-900"}
          m-1 relative inline-flex flex-shrink-0 h-[25px] w-[45px] border-2 border-transparent rounded-full cursor-pointer transition-colors ease-in-out duration-200 focus:outline-none focus-visible:ring-2  focus-visible:ring-white focus-visible:ring-opacity-75`}
          >
            <span className="sr-only">Use setting</span>
            <span
              aria-hidden="true"
              className={`${isStatement ? "translate-x-5" : "translate-x-0"}
            pointer-events-none inline-block h-[21px] w-[21px] rounded-full bg-white shadow-lg transform ring-0 transition ease-in-out duration-200`}
            />
          </Switch>
        </label>
        <div className="w-full m-4 content-center">
          <RuleTypeSelector ruleType={ruleType} setRuleType={setRuleType} />
        </div>
        <div className="relative block m-4 p-3 border-2 border-gray-200 rounded-lg h-100px">
          <ExtraRuleFields
            ruleType={ruleType}
            getField={(k, d) => ruleExtraFields[k] || d}
            setField={(k, v) =>
              setRuleExtraFields({ ...ruleExtraFields, [k]: v })
            }
          />
        </div>
        <div className="w-full place-items-center">
          <button
            className="m-5 max-w-[200px] inline-block px-8 py-3 text-sm font-medium text-white transition bg-indigo-600 rounded hover:scale-110 hover:shadow-xl active:bg-indigo-500 focus:outline-none focus:ring"
            onClick={onSubmit}
          >
            Add Rule
          </button>
        </div>
        {errorMessage && <div className="text-rose-400">{errorMessage}</div>}
      </div>
    </>
  );
};

export default AddRule;
