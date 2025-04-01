using System.Text;
using MiniFramework.Core.Metadata;
using MiniFramework.Core;

namespace MiniFramework.CodeGen;

public static class DIRegistrationGenerator
{
    public static string Generate(IEnumerable<EntityMetadata> entities, string @namespace = "MyApp.DependencyInjection")
    {
        var sb = new StringBuilder();

        sb.AppendLine("using Microsoft.Extensions.DependencyInjection;");
        sb.AppendLine();
        sb.AppendLine($"namespace {@namespace}");
        sb.AppendLine("{");
        sb.AppendLine("    public static class RepositoryRegistration");
        sb.AppendLine("    {");
        sb.AppendLine("        public static IServiceCollection AddGeneratedRepositories(this IServiceCollection services)");
        sb.AppendLine("        {");

        foreach (var meta in entities)
        {
            if (!meta.GenerateRepository) continue;

            string name = meta.Name;
            //sb.AppendLine($"            services.AddScoped<I{name}Repository, {name}Repository>();");
            if (!meta.GenerateRepository || !meta.RegisterService)
                continue;

            var interfaceName = $"I{meta.Name}Repository";
            var className = $"{meta.Name}Repository";

            string method = meta.ServiceLifetime switch
            {
                ServiceLifetimeOption.Singleton => "AddSingleton",
                ServiceLifetimeOption.Transient => "AddTransient",
                _ => "AddScoped"
            };

            sb.AppendLine($"            services.{method}<{interfaceName}, {className}>();");
        }

        sb.AppendLine();
        sb.AppendLine("            return services;");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");

        return sb.ToString();
    }
}
