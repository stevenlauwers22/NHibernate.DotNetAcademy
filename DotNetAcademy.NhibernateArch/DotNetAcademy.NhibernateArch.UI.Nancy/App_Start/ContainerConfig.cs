using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DotNetAcademy.NhibernateArch.Contracts;
using DotNetAcademy.NhibernateArch.Domain.Handlers;
using DotNetAcademy.NhibernateArch.Infrastructure.Handlers;
using NHibernate;

namespace DotNetAcademy.NhibernateArch.UI.Nancy.App_Start
{
    public class ContainerConfig
    {
        public static void RegisterDependencies(IWindsorContainer container)
        {
            container
                //.Register(
                //    Component
                //        .For<IWindsorContainer>()
                //        .Instance(container)
                //        .LifestyleSingleton())
                .Register(
                    Component
                        .For<IDispatcher>()
                        .ImplementedBy<Dispatcher>()
                        .LifestyleTransient())
                .Register(
                    AllTypes
                        .FromAssemblyNamed("DotNetAcademy.NhibernateArch.Domain")
                        .BasedOn<IHandler>()
                        .WithService
                        .AllInterfaces()
                        .LifestyleTransient())
                .Register(
                    Component
                        .For<IPostService>()
                        .ImplementedBy<PostService>()
                        .LifestyleTransient())
                .Register(
                    Component
                        .For<ISessionFactory>()
                        .UsingFactoryMethod(fm =>
                        {
                            var configuration = new NHibernate.Cfg.Configuration().Configure();
                            var sessionFactory = configuration.BuildSessionFactory();
                            return sessionFactory;
                        })
                        .LifestyleSingleton())
                .Register(
                    Component
                        .For<ISession>()
                        .UsingFactoryMethod(fm => fm.Resolve<ISessionFactory>().OpenSession())
                        .LifestylePerWebRequest())
                ;
        }
    }
}