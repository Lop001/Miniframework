using MiniFramework.Core;
using MiniFramework.Core.Attributes;

public interface IEmailService
{
    void SendEmail(string to, string message);
}

[Service(Lifetime = ServiceLifetimeOption.Singleton, AsInterface = true)]
public class EmailService : IEmailService
{
    public void SendEmail(string to, string message)
    {
        Console.WriteLine($"📨 -> {to}: {message}");
    }
}
