namespace MiniFramework.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class FieldAttribute : Attribute
{
    public bool Required { get; set; }
    public string? DisplayName { get; set; }

    public FieldAttribute(bool required = false, string? displayName = null)
    {
        Required = required;
        DisplayName = displayName;
    }
}
