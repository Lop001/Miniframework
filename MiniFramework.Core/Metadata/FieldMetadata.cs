using System.Reflection;
using MiniFramework.Core.Attributes;

namespace MiniFramework.Core.Metadata;

public class FieldMetadata
{
    public PropertyInfo Property { get; set; } = null!;
    public string Name => Property.Name;
    public string? DisplayName { get; set; }
    public bool Required { get; set; }
    public bool IsPrimaryKey { get; set; }
    public bool IsForeignKey => Relation != null;
    public RelationAttribute? Relation { get; set; }
    public string Type => Property.PropertyType.Name;
}
