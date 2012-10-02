using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace DotNetAcademy.NhibernateArch.Infrastructure.Windsor
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return base.GetControllerInstance(requestContext, null);
            
            var controller = _container.Resolve(controllerType) as Controller;
            if (controller != null)
                controller.ActionInvoker = _container.Resolve<IActionInvoker>();
            
            return controller;
        }

        public override void ReleaseController(IController controller)
        {
            _container.Release(controller);
        }
    }
}