namespace MiniFramework.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ServiceAttribute : Attribute
{
    public bool Register { get; set; } = true;
    public ServiceLifetimeOption Lifetime { get; set; } = ServiceLifetimeOption.Scoped;
    public bool AsInterface { get; set; } = true;

    public ServiceAttribute(bool register = true)
    {
        Register = register;
    }
}