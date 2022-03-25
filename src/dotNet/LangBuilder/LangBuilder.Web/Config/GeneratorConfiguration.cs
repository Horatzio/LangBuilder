using System.Text.Json;

namespace LangBuilder
{
    public record GeneratorConfigurationModel
        (
        string antlrCommandLine,
        string antlrPath,
        string transpilerProjectPath,
        string executablePath)
    {
    }

    public record GeneratorConfiguration
    {
        private GeneratorConfigurationModel model;
        public GeneratorConfiguration(GeneratorConfigurationModel model)
        {
            this.model = model;
        }

        public string AntlrCommandLine => model.antlrCommandLine;

        public string AntlrPath => model.antlrPath;

        public string TranspilerProjectPath => model.transpilerProjectPath;

        public string GrammarFilePath => $"{model.transpilerProjectPath}/Grammar/TranspilerGrammar.g4";

        public string AntlrOutputPath => $"{model.transpilerProjectPath}/Output";

        public string ExecutablePath => model.executablePath;
    }

    public class GeneratorConfigurationBuilder
    {
        private const string ConfigFilePath = "./Config/generatorsettings.json";

        public GeneratorConfiguration Build()
        {
            var model = JsonSerializer.Deserialize<GeneratorConfigurationModel>(File.ReadAllText(ConfigFilePath));
            if (model == null) throw new Exception("Unable to deserialize configuration");
            return new GeneratorConfiguration(model);
        }
    }
}
