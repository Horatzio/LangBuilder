using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using LangBuilder.Data;

namespace LangBuilder.Web
{
    public class AutofacConfig
    {
        public static void ConfigureDependencyInjection(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterModelBinderProvider();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(configuration);
            builder.RegisterWebApiModelBinderProvider();

            #region Types
            builder.RegisterType<LangBuilderContext>().As<LangBuilderContext>();
            #endregion

            var container = builder.Build();
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}