using LangBuilder.Source.Service;
using System.Configuration;

namespace LangBuilder.Web
{
    public static class ServiceConfiguration
    {
        public static void Configure(this IServiceCollection services)
        {
            services.Configure<GeneratorConfiguration>();
            services.AddSingleton<AntlrGeneratorService>();
            services.AddSingleton<ExecutableGeneratorService>();
            services.AddSingleton<GrammarFileGeneratorService>();
            services.AddSingleton<TranspilerRuleService>();
        }
    }
}
