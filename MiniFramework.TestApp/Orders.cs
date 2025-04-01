using MiniFramework.Core.Attributes;
using MiniFramework.Core;

namespace MiniFramework.TestApp;
[Entity(displayName: "Objednávka", tableName: "Orders")]
[Repository(generate: true, RegisterService = true, Lifetime = ServiceLifetimeOption.Transient)]
[AuthorizeAction(CrudAction.Create, "Admin", "Editor")]
[AuthorizeAction(CrudAction.Delete, "Admin")]
[AuthorizeAction(CrudAction.GetAll)] // jen přihlášení
public class Order
{
    [PrimaryKey]
    public int Id { get; set; }

    [Field(Required = true)]
    [Relation("Users")]
    public int CustomerId { get; set; }

    [Field]
    public DateTime CreatedAt { get; set; }
}