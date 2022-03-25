namespace LangBuilder.Models
{
    public class AntlrGenerateOutputModel
    {
        public int ExitCode { get; set; }
        public string Output { get; set; }
        public string Error { get; set; }
    }
}
