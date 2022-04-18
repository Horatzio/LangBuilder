import { useState, useCallback } from "react";
import { TranspilerRule } from "../../api/transpiler-rule";
import hljs from "highlight.js";

interface RuleSet {
  name: string;
  rules: TranspilerRule[];
}

const TranspilerRunner: React.FC = () => {
  const [ruleSet, setRuleSet] = useState<RuleSet>();
  const [sourceText, setSourceText] = useState("");

  const getHighlightedSourceText = useCallback(() => {
    const result = hljs.highlight(sourceText, { language: "cpp" });
    return result.value;
  }, [sourceText]);

  const unexpected = () => {
    throw new Error();
  };
  const readFile = async (f: File) => {
    const val = Buffer.from(await f.arrayBuffer()).toString("utf-8");
    const ruleSet = JSON.parse(val);
    setRuleSet(ruleSet);
  };

  return (
    <>
      <div className="flex h-fit">
        <div className="w-full flex-1 m-5 bg-white shadow overflow-hidden sm:rounded-lg">
          <div className="w-100 h-60 m-10 place-items-center">
            <div className="w-fit h-fit">
              <form
                data-single="true"
                className="dropzone border-gray-200 border-dashed border-2 p-5"
              >
                <div className="fallback">
                  <input
                    name="file"
                    type="file"
                    accept="application/json"
                    onChange={(e) =>
                      readFile(
                        e.target.files ? e.target.files[0] : unexpected()
                      )
                    }
                  />
                </div>
              </form>
            </div>
          </div>
          <div className="flex">
            <div className="flex-1 m-5 bg-white shadow overflow-hidden sm:rounded-lg">
              <input
                type="textbox"
                className="h-25"
                onChange={({ target: { value } }) => setSourceText(value)}
                value={sourceText}
              />
              <p>{getHighlightedSourceText()}</p>
            </div>
            <div className="flex-1 m-5 bg-white shadow overflow-hidden sm:rounded-lg">
              <text className="h-25">asfasf</text>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default TranspilerRunner;
