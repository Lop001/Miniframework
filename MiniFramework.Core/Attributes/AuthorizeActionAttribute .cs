namespace MiniFramework.Core.Attributes;

public enum CrudAction
{
    GetAll,
    GetById,
    Create,
    Update,
    Delete
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizeActionAttribute : Attribute
{
    public CrudAction Action { get; }
    public string? Role { get; }
    public string? Policy { get; }

    public AuthorizeActionAttribute(CrudAction action, string? role = null, string? policy = null)
    {
        Action = action;
        Role = role;
        Policy = policy;
    }
}
