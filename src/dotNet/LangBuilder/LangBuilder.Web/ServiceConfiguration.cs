using LangBuilder.Source.Service;

namespace LangBuilder.Web
{
    public static class ServiceConfiguration
    {
        public static void Configure(this IServiceCollection services)
        {
            services.AddSingleton<GeneratorConfigurationFactory>();
            services.AddSingleton((provider) => provider.GetRequiredService<GeneratorConfigurationFactory>().Build());
            services.AddSingleton<AntlrGeneratorService>();
            services.AddSingleton<ExecutableGeneratorService>();
            services.AddSingleton<GrammarFileGeneratorService>();
            services.AddSingleton<TranspilerRuleService>();
            services.AddSingleton<TranspilerGeneratorService>();
        }
    }
}
