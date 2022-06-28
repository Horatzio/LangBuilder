interface CodeOutputProps {
  value: string;
}

const CodeOutput: React.FC<CodeOutputProps> = ({ value }) => {
  return (
    <>
      <div className="max-w-7xl mx-auto bg-gray-800 rounded-lg h-full">
        <div className="py-3 border-b border-gray-500/30 flex place-content-between">
          <div className="flex space-x-1.5 px-3">
            <div className="w-2.5 h-2.5 rounded-full bg-gray-600"></div>
            <div className="w-2.5 h-2.5 rounded-full bg-gray-600"></div>
            <div className="w-2.5 h-2.5 rounded-full bg-gray-600"></div>
          </div>
          <div className="text-gray-300 mr-5">Output</div>
        </div>
        <div className="flex">
          <div className="w-full overflow-hidden">
            <div className="p-5 text-gray-300">
              <div className="bg-gray-800 text-gray-300 w-full h-fit">
                {value}
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default CodeOutput;
