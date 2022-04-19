using System.Diagnostics;

namespace LangBuilder.Web
{
    public class TranspilerRunnerService
    {
        public async Task<RunResultDto> Run(string executablePath, string sourceText)
        {
            using var process = new Process()
            {
                StartInfo =
                {
                    FileName = "dotnet",
                    Arguments = string.Join(" ", new [] { Path.GetFileName(executablePath), "-c"}),
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = Path.GetDirectoryName(executablePath)
                }
            };

            process.Start();
            await process.StandardInput.WriteAsync(sourceText);
            process.StandardInput.Close();

            using var cancellationTokenSource = new CancellationTokenSource();
            var waitForExit = process.WaitForExitAsync(cancellationTokenSource.Token);
            cancellationTokenSource.CancelAfter(7500);

            try
            {
                await waitForExit;
            } catch (TaskCanceledException)
            {
            }

            var error = "";
            if (process.ExitCode != 0)
                error = await process.StandardError.ReadToEndAsync();
            var result = await process.StandardOutput.ReadToEndAsync();
            return new RunResultDto
            {
                TranspiledText = result,
                Error = error
            };
        }
    }

    public class RunResultDto
    {
        public string TranspiledText { get; set; }
        public string Error { get; set; }
    }
}