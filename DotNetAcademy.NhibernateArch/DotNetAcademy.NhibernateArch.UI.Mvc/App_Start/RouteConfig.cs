using System.Web.Mvc;
using System.Web.Routing;

namespace DotNetAcademy.NhibernateArch.UI.Mvc.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Posts", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}