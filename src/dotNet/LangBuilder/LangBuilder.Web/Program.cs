using LangBuilder.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddCors();

builder.Services.Configure();

var app = builder.Build();

app.UseCors(o => o.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials());

app.MapControllers();

app.Run();
