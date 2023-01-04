import Editor from "react-simple-code-editor";

interface CodeInputProps {
  value: string;
  onChange: (newValue: string) => any;
}

const CodeInput: React.FC<CodeInputProps> = ({ value, onChange }) => {
  return (
    <>
      <div className="max-w-7xl mx-auto bg-gray-800 rounded-lg">
        <div className="py-3 border-b border-gray-500/30 flex place-content-between">
          <div className="flex space-x-1.5 px-3">
            <div className="w-2.5 h-2.5 rounded-full bg-gray-600"></div>
            <div className="w-2.5 h-2.5 rounded-full bg-gray-600"></div>
            <div className="w-2.5 h-2.5 rounded-full bg-gray-600"></div>
          </div>
          <div className="text-gray-300 mr-5">Input</div>
        </div>
        <div className="flex">
          <div className="w-full overflow-hidden">
            <div className="p-5 text-gray-300">
              <Editor
                value={value}
                onValueChange={(value) => onChange(value)}
                highlight={(c) => c}
                padding={5}
                tabSize={2}
                className="bg-gray-800 text-gray-300 w-full h-fit outline-none"
              ></Editor>
            </div>
            <div className="border-t border-gray-500/30 font-mono text-gray-200 text-xs p-4 space-y-2">
              <h3>Problems</h3>
              <ul className="text-gray-300 space-y-1"></ul>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default CodeInput;

/*
construct Solution
<
  operation Main {text[] args}
  <
    display["HelloWorld"]
  >
>
*/
