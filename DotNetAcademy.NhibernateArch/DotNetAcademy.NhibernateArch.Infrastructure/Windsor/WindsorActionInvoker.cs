using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Windsor;

namespace DotNetAcademy.NhibernateArch.Infrastructure.Windsor
{
    public class WindsorActionInvoker : ControllerActionInvoker
    {
        private readonly IWindsorContainer _container;

        public WindsorActionInvoker(IWindsorContainer container)
        {
            _container = container;
        }

        protected override ActionExecutedContext InvokeActionMethodWithFilters(ControllerContext controllerContext, IList<IActionFilter> filters, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            var filterAttributes = filters.Where(f => !(f is IController)).ToList();
            foreach (var filterAttribute in filterAttributes)
            {
                _container.Kernel.InjectProperties(filterAttribute);
            } 

            return base.InvokeActionMethodWithFilters(controllerContext, filters, actionDescriptor, parameters); 
        }
    }
}