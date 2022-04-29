using LangBuilder.Source.Models;
using Microsoft.AspNetCore.Mvc;

namespace LangBuilder.Web;

[ApiController]
[Route("api")]
public class WebApiController
{
    private readonly TranspilerRunnerService transpilerRunnerService;
    private readonly TranspilerGeneratorService transpilerGeneratorService;
    private readonly IConfiguration configuration;
    private readonly string webCachePath;

    public WebApiController(TranspilerGeneratorService service, IConfiguration configuration, TranspilerRunnerService transpilerRunnerService)
    {
        this.transpilerGeneratorService = service;
        this.configuration = configuration;
        webCachePath = configuration.GetValue<string>("WebCachePath");
        this.transpilerRunnerService = transpilerRunnerService;
    }


    [HttpPost("transpile")]
    // /api/transpile
    public async Task<object> Transpile([FromBody] TranspileDto model)
    {
        var path = GetExecutablePath(model.Name);
        if (!File.Exists(path))
            throw new Exception("Unable to find executable!");

        return await transpilerRunnerService.Run(path, model.SourceText);
    }

    [HttpPost("create-transpiler")]
    public async Task<object> CreateTranspiler([FromBody] TranspilerViewModel model)
    {
        var path = GetExecutablePath(model.Name);
        if (File.Exists(path))
            return null;

        return await transpilerGeneratorService.GenerateTranspiler(model, path);
    }

    private string GetExecutablePath(string name)
    {
        return Path.Combine(
            Path.GetDirectoryName(webCachePath),
            Path.ChangeExtension(name, Path.GetExtension(webCachePath))
            );
    }
}

public class TranspileDto
{
    public string Name { get; set; }
    public string SourceText { get; set; }
}
