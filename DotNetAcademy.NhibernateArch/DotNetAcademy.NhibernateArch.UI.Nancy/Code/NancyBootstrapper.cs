using Castle.Windsor;
using DotNetAcademy.NhibernateArch.UI.Nancy.App_Start;
using Nancy.Bootstrappers.Windsor;

namespace DotNetAcademy.NhibernateArch.UI.Nancy.Code
{
    public class NancyBootstrapper : WindsorNancyBootstrapper
    {
        protected override byte[] FavIcon
        {
            get { return null; }
        }

        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            ContainerConfig.RegisterDependencies(existingContainer);
            base.ConfigureApplicationContainer(existingContainer);
        }
    }
}