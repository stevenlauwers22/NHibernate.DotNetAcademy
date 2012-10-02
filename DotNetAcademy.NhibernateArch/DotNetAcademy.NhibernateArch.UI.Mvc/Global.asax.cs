using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using DotNetAcademy.NhibernateArch.Infrastructure.Windsor;
using DotNetAcademy.NhibernateArch.UI.Mvc.App_Start;

namespace DotNetAcademy.NhibernateArch.UI.Mvc
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = new WindsorContainer();
            ContainerConfig.RegisterDependencies(container);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}