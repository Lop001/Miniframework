namespace MiniFramework.Core.Metadata;

public class ServiceMetadata
{
    public Type ImplementationType { get; set; } = null!;
    public string? InterfaceName { get; set; }
    public ServiceLifetimeOption Lifetime { get; set; }
}