using LangBuilder.Source.Models;
using LangBuilder.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure();

var app = builder.Build();

app.MapPost("/api/test-transpile",
    async (TranspilerViewModel model, TranspilerGeneratorService service) 
    => await service.TestGenerateTranspiler(model));

app.Run();
