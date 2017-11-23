using System.Web.Http;
using Dpic.Connect.WebApi.Infrastructure;

namespace Dpic.Connect.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            GlobalConfiguration.Configuration.Filters.Add(
                new WebApiExceptionHandlingAttribute());
        }
    }
}
