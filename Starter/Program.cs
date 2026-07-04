namespace Starter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("hiii");
        }
    }
}


public interface IMessageSender
{
    public Task SendAsync(string subject, string body, string recipient);
}

public class SmsSender : IMessageSender
{
    public Task SendAsync(string subject, string body, string recipient)
    {
        Console.WriteLine($"subject: {subject} , body: {body}, recipient: {recipient}");
        return Task.CompletedTask;
    }
}

public class EmailSender : IMessageSender
{
    public Task SendAsync(string subject, string body, string recipient)
    {
        Console.WriteLine($"subject: {subject} , body: {body}, recipient: {recipient}");
        return Task.CompletedTask;
    }
}


public abstract class Notification
{
    protected  readonly IMessageSender Sender;

    protected Notification(IMessageSender sender)
    {
        this.Sender = sender;
    }

    public abstract Task NotifyAsync(string recipient, string message);
}

public class AlertNotification : Notification
{
    public AlertNotification(IMessageSender sender) : base(sender)
    {
    }

    public override Task NotifyAsync(string recipient, string message)
    {
       return Sender.SendAsync("alert " , recipient, message);
    }
}

public class ReminderNotification : Notification
{
    public ReminderNotification(IMessageSender sender) : base(sender)
    {
    }

    public override Task NotifyAsync(string recipient, string message)
    {
        return Sender.SendAsync("reminder ", recipient, message);
    }
}
