using System.Web.Http;
using TkMiddleware.Security;

namespace TkMiddleware
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Authorization
            config.Filters.Add(new ApiKeyAuthenticationAttribute());
            config.SuppressHostPrincipal();
        }
    }
}
