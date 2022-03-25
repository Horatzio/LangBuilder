namespace LangBuilder.Models
{
    public class GenerateExecutableOutputModel
    {
        public bool Success { get; set; }
        public IEnumerable<GenerateExecutableDiagnosticError> Errors { get; set; }
    }

    public class GenerateExecutableDiagnosticError
    {
        public string Severity { get; set; }
        public string ErrorId { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public GenerateExecutableDiagnosticErrorLocation Location { get; set; }
    }

    public class GenerateExecutableDiagnosticErrorLocation
    {
        public int StartLine { get; set; }
        public int StartPosition { get; set; }
        public int EndLine { get; set; }
        public int EndPosition { get; set; }
    }
}
