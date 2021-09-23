namespace LangBuilder
{
    public class GeneratorConfiguration
    {
        public string AntlrCommandLine { get; set; }
        public string AntlrPath { get; set; }
        public string GrammarFilePath { get; set; }
        public string AntlrOutputPath { get; set; }
        public string TranspilerProgramPath { get; set; }
        public string ExecutablePath { get; set; }
        public string TranspilerSolutionPath { get; set; }
    }
}
