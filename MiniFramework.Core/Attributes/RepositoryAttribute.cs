namespace MiniFramework.Core.Attributes;


[AttributeUsage(AttributeTargets.Class)]
public class RepositoryAttribute : Attribute
{
    public bool Generate { get; set; } = true;
    public bool ReadOnly { get; set; } = false;
    public bool RegisterService { get; set; } = true;
    public ServiceLifetimeOption Lifetime { get; set; } = ServiceLifetimeOption.Scoped;

    public RepositoryAttribute(bool generate = true, bool readOnly = false)
    {
        Generate = generate;
        ReadOnly = readOnly;
    }
}
