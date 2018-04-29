using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Net;
using System.Security;
using System.IO;
using System.Reflection;

namespace obudai_webapp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // remove xml response type so it defaults to JSON only
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(
                t => t.MediaType == "application/xml"
            );
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // add api token filter
            config.Filters.Add(new AuthFilter());
        }
    }
}
