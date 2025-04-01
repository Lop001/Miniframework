using System.Reflection;
using MiniFramework.Core.Attributes;

namespace MiniFramework.Core.Reflection;

public static class EntityDiscovery
{
    public static IEnumerable<Type> FindAllEntities(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t.IsClass && t.GetCustomAttribute<EntityAttribute>() != null);
    }
}
