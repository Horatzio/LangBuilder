import { useState, useEffect } from "react";
import { TranspilerRule } from "../../api/transpiler-rule";
import "highlight.js/styles/github.css";
import axios from "axios";
import apiUrl from "../../api-url";
import * as Buffer from "buffer";

interface RuleSet {
  name: string;
  rules: TranspilerRule[];
}

interface TranspileResponse {
  transpiledText: string;
}

const TranspilerRunner: React.FC = () => {
  const [ruleSet, setRuleSet] = useState<RuleSet>();
  const [sourceText, setSourceText] = useState("");
  const [transpiledText, setTranspiledText] = useState("");

  useEffect(() => {
    if (!ruleSet) return;

    createTranspiler(ruleSet);

    async function createTranspiler(ruleSet: RuleSet) {
      await axios.post(apiUrl("api/create-transpiler"), ruleSet);
    }
  }, [ruleSet]);

  useEffect(() => {
    if (!ruleSet) return;
    if (!sourceText) return;

    transpileText(ruleSet);

    async function transpileText(ruleSet: RuleSet) {
      const response = await axios.post<TranspileResponse>(
        apiUrl("api/transpile"),
        {
          name: ruleSet.name,
          sourceText: sourceText,
        }
      );

      setTranspiledText(response.data.transpiledText);
    }
  }, [ruleSet, sourceText]);

  // const getHighlightedSourceText = useCallback(() => {
  //   const result = hljs.highlight(sourceText, { language: "c" });
  //   return result.value;
  // }, [sourceText]);

  const unexpected = () => {
    throw new Error();
  };
  const readFile = async (f: File) => {
    const aBuffer = await f.arrayBuffer();
    const val = String.fromCharCode.apply(null, new Uint8Array(aBuffer) as any);
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
                className="h-[100px] w-[200px]"
                onChange={({ target: { value } }) => setSourceText(value)}
                value={sourceText}
              />
            </div>
            <div className="flex-1 m-5 bg-white shadow overflow-hidden sm:rounded-lg">
              <text className="h-[100px] w-[200px]">{transpiledText}</text>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default TranspilerRunner;