using System.Reflection;
using Castle.MicroKernel;

namespace DotNetAcademy.NhibernateArch.Infrastructure.Windsor
{
    public static class WindsorKernelExtension
    {
        public static void InjectProperties(this IKernel kernel, object target)
        {
            var type = target.GetType();
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!property.CanWrite || !kernel.HasComponent(property.PropertyType))
                    continue;

                var value = kernel.Resolve(property.PropertyType);
                property.SetValue(target, value, null);
            }
        }
    }
}