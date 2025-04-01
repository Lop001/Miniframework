namespace MiniFramework.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class RelationAttribute : Attribute
{
    public string TargetEntity { get; }
    public string? TargetKey { get; }

    public RelationAttribute(string targetEntity, string? targetKey = "Id")
    {
        TargetEntity = targetEntity;
        TargetKey = targetKey;
    }
}