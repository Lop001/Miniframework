using MiniFramework.Core.Attributes;

namespace MiniFramework.TestApp;

[Entity(DisplayName = "Zákazník", TableName ="Customers")]
public class Customer
{
    [PrimaryKey]
    public int Id { get; set; }

    [Field(Required = true, DisplayName = "Jméno")]
    public string FirstName { get; set; }

    [Field(Required = true, DisplayName = "Příjmení")]
    public string LastName { get; set; }

    [Field(DisplayName = "E-mail")]
    public string Email { get; set; }

    [Field(DisplayName = "Datum registrace")]
    public DateTime RegisteredAt { get; set; }
}
