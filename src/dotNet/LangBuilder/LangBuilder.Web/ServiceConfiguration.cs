using LangBuilder.Source.Service;

namespace LangBuilder.Web
{
    public static class ServiceConfiguration
    {
        public static void Configure(this IServiceCollection services)
        {
            services.AddSingleton<GeneratorConfigurationBuilder>();
            services.AddSingleton((provider) => provider.GetRequiredService<GeneratorConfigurationBuilder>().Build());
            services.AddSingleton<AntlrGeneratorService>();
            services.AddSingleton<ExecutableGeneratorService>();
            services.AddSingleton<GrammarFileGeneratorService>();
            services.AddSingleton<TranspilerRuleService>();
            services.AddSingleton<TranspilerGeneratorService>();
            services.AddSingleton<TranspilerRunnerService>();
        }
    }
}
