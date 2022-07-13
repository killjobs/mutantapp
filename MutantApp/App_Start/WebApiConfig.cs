using System.Web.Http.Cors;
using System.Web.Http;

namespace MutantApp
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
