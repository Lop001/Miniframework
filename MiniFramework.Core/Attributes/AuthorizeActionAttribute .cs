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
    public string[] Roles { get; }
    public string? Policy { get; set; }

    public AuthorizeActionAttribute(CrudAction action, params string[] roles)
    {
        Action = action;
        Roles = roles;
    }

    public AuthorizeActionAttribute(CrudAction action)
    {
        Action = action;
        Roles = Array.Empty<string>();
    }
}
