using System.Reflection;
using MiniFramework.Core.Attributes;
using MiniFramework.Core.Metadata;

namespace MiniFramework.Core.Reflection;

public static class ServiceDiscovery
{
    public static IEnumerable<ServiceMetadata> FindAllServices(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
        {
            var attr = type.GetCustomAttribute<ServiceAttribute>();
            if (attr == null || !attr.Register)
                continue;

            var metadata = new ServiceMetadata
            {
                ImplementationType = type,
                Lifetime = attr.Lifetime,
                InterfaceName = attr.AsInterface
                    ? type.GetInterfaces().FirstOrDefault(i => i.Name == $"I{type.Name}")?.Name
                    : null
            };

            yield return metadata;
        }
    }
}
