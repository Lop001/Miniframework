using System.Reflection;
using MiniFramework.Core.Attributes;

namespace MiniFramework.Core.Metadata;

public static class EntityMetadataExtractor
{
    public static EntityMetadata Extract<T>() => Extract(typeof(T));

    public static EntityMetadata Extract(Type type)
    {
        var entityAttr = type.GetCustomAttribute<EntityAttribute>();
        if (entityAttr == null)
            throw new InvalidOperationException($"Type {type.Name} is not marked as [Entity]");


        var repoAttr = type.GetCustomAttribute<RepositoryAttribute>();

        var metadata = new EntityMetadata
        {
            Name = type.Name,
            DisplayName = entityAttr.DisplayName, 
            TableName = entityAttr.TableName ?? type.Name + "s",
            GenerateRepository = repoAttr?.Generate ?? true,
            IsReadOnly = repoAttr?.ReadOnly ?? false,
            RegisterService = repoAttr?.RegisterService ?? true,
            ServiceLifetime = repoAttr?.Lifetime ?? ServiceLifetimeOption.Scoped,
            AuthorizeAttributes = type.GetCustomAttributes<AuthorizeAccessAttribute>().ToList(),
            ActionAuthorizations = type.GetCustomAttributes<AuthorizeActionAttribute>().ToList()

        };

        

        foreach (var prop in type.GetProperties())
        {
            var fieldAttr = prop.GetCustomAttribute<FieldAttribute>();
            var pkAttr = prop.GetCustomAttribute<PrimaryKeyAttribute>();
            var relationAttr = prop.GetCustomAttribute<RelationAttribute>();

            if (fieldAttr == null && pkAttr == null && relationAttr == null)
                continue;

            var fieldMeta = new FieldMetadata
            {
                Property = prop,
                DisplayName = fieldAttr?.DisplayName ?? prop.Name,
                Required = fieldAttr?.Required ?? false,
                IsPrimaryKey = pkAttr != null,
                Relation = relationAttr
            };

            if (pkAttr != null)
                metadata.PrimaryKey = prop;

            metadata.Fields.Add(fieldMeta);
        }

        return metadata;
    }
}
