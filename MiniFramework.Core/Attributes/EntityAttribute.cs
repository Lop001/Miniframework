namespace MiniFramework.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EntityAttribute : Attribute
{
    public string? DisplayName { get; set; }
    public string? TableName { get; set; }

    public EntityAttribute(string? displayName = null)
    {
        DisplayName = displayName;
    }

    public EntityAttribute(string? displayName, string? tableName)
    {
        DisplayName = displayName;
        TableName = tableName;
    }
    
}
