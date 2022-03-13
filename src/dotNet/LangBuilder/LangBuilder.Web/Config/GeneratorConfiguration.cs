using System.Text.Json;

namespace LangBuilder
{
    public record GeneratorConfiguration
        (
        string AntlrCommandLine,
        string AntlrPath,
        string GrammarFilePath,
        string AntlrOutputPath,
        string TranspilerProgramPath,
        string ExecutablePath,
        string TranspilerSolutionPath)
    {
    }

    public class GeneratorConfigurationFactory
    {
        private const string ConfigFilePath = "./Config/generatorsettings.json";

        public GeneratorConfiguration Build()
        {
            var config = JsonSerializer.Deserialize<GeneratorConfiguration>(File.ReadAllText(ConfigFilePath));
            if (config == null) throw new Exception("Unable to deserialize configuration");
            return config;
        }
    }
}
