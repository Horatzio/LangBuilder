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
              <ul className="text-gray-300 space-y-1">
                <li className="flex space-x-2 items-center">
                  <svg
                    width="24"
                    height="24"
                    fill="none"
                    className="flex-none text-yellow-400"
                  >
                    <path
                      d="m5.207 16.203 5.072-10.137c.711-1.422 2.736-1.421 3.447 0l5.067 10.137c.642 1.285-.29 2.797-1.723 2.797H6.93c-1.434 0-2.366-1.513-1.723-2.797ZM12 10v2"
                      stroke="currentColor"
                      stroke-width="2"
                      stroke-linecap="round"
                      stroke-linejoin="round"
                    ></path>
                    <path
                      d="M12.5 16a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0Z"
                      stroke="currentColor"
                    ></path>
                  </svg>
                  <p>'flex' applies the same CSS property as 'block'.</p>
                  <p className="text-gray-500">cssConflict [1, 20]</p>
                </li>
                <li className="flex space-x-2 items-center">
                  <svg
                    width="24"
                    height="24"
                    fill="none"
                    className="flex-none text-yellow-400"
                  >
                    <path
                      d="m5.207 16.203 5.072-10.137c.711-1.422 2.736-1.421 3.447 0l5.067 10.137c.642 1.285-.29 2.797-1.723 2.797H6.93c-1.434 0-2.366-1.513-1.723-2.797ZM12 10v2"
                      stroke="currentColor"
                      stroke-width="2"
                      stroke-linecap="round"
                      stroke-linejoin="round"
                    ></path>
                    <path
                      d="M12.5 16a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0Z"
                      stroke="currentColor"
                    ></path>
                  </svg>
                  <p>'block' applies the same CSS property as 'flex'.</p>
                  <p className="text-gray-500">cssConflict [1, 54]</p>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default CodeInput;
