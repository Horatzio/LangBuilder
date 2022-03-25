using LangBuilder.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.Configure();

var app = builder.Build();

app.MapControllers();

app.Run();
