import TranspilerBuilder from "../components/transpiler-builder/TranspilerBuilder";
import TranspilerRunner from "../components/transpiler-runner/TranspilerRunner";

const Dashboard: React.FC = () => {
  return (
    <>
      {/* <div className="px-4 py-6 sm:px-0">
        <div className="border-4 border-dashed border-gray-200 rounded-lg h-96" />
      </div> */}
      <div className="py-6">
        <header className="bg-white shadow">
          <div className="max-w-7xl mx-auto py-6 px-4 sm:px-6 lg:px-8">
            <h1 className="text-3xl font-bold text-gray-900">Builder</h1>
          </div>
        </header>
        <TranspilerBuilder />
        <header className="bg-white shadow">
          <div className="max-w-7xl mx-auto py-6 px-4 sm:px-6 lg:px-8">
            <h1 className="text-3xl font-bold text-gray-900">Runner</h1>
          </div>
        </header>
        <TranspilerRunner />
      </div>
    </>
  );
};
export default Dashboard;
