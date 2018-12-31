using AADx.EventsApi.DAL;
using AADx.TodosApi.DAL;
using AADx.UsersApi.DAL;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace AADx.TodoApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // DI Container
            var container = new UnityContainer();
            container.RegisterType<ITodoRepository, LiteTodoRepository>(new SingletonLifetimeManager());
            container.RegisterType<IUserRepository, LiteUserRepository>(new SingletonLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // serialize to camelCase
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
