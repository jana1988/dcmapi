using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using DCMAPI.Attributes;

namespace DCMAPI
{
    /// <summary>
    /// Common standard configuration for all API project is controlled using this component
    /// </summary>
    public static class BaseWebApiConfig
    {
        public static void Register(HttpConfiguration config, bool isRequiredEnableQuery = true)
        {
            // Web API routes & attributes
            if (config != null)
            {
                config.MapHttpAttributeRoutes();
                config.Filters.Add(new AuthorizeAttribute());

                // JSON serialization settings
                config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                var corsAttr = new EnableCorsAttribute("*", "*", "*");
                config.EnableCors(corsAttr);
                //CORS settings
                //config.MessageHandlers.Add(new CorsHandler());
            }
        }
    }
}
