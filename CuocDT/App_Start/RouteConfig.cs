using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CuocDT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Ghi Log Success",
                url: "success-ghilog",
                defaults: new { controller = "Log", action = "SuccessGhiLog", id = UrlParameter.Optional },
                namespaces: new[] { "CuocDT.Controllers" }
            );

            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Cuoc", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "CuocDT.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "CuocDT.Controllers" }
            );
        }
    }
}
