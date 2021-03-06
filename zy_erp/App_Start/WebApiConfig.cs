using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace zy_erp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //解决跨域
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // Web API 配置和服务
            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
