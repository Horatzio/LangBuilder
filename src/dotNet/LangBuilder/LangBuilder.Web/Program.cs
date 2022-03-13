using LangBuilder.Web;
using LangBuilder.Web.src;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure();

var app = builder.Build();

app.MapPost("/api/test-transpile", (TranspilerGeneratorService service) => service.TestGenerateTranspiler);

app.Run();
