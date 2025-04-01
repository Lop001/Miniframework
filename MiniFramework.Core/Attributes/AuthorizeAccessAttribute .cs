namespace MiniFramework.Core.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeAccessAttribute : Attribute
{
    public string? Policy { get; }
    public string? Role { get; }

    public AuthorizeAccessAttribute(string? policy = null, string? role = null)
    {
        Policy = policy;
        Role = role;
    }
}
