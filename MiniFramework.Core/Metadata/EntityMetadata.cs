using System.Reflection;
using MiniFramework.Core.Attributes;

namespace MiniFramework.Core.Metadata;

public class EntityMetadata
{
    public string Name { get; set; } = "";
    public string? DisplayName { get; set; }
    public PropertyInfo PrimaryKey { get; set; } = null!;
    public string TableName { get; set; } = "";
    public List<FieldMetadata> Fields { get; set; } = new();

    public bool GenerateRepository { get; set; } = true;
    public bool IsReadOnly { get; set; } = false;

    public bool RegisterService { get; set; } = true;
    public ServiceLifetimeOption ServiceLifetime { get; set; } = ServiceLifetimeOption.Scoped;
    public List<AuthorizeAccessAttribute> AuthorizeAttributes { get; set; } = new();

}
