namespace Starter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var emailCreator = new EmailCreator();
            emailCreator.SendNotification("Sending Email");

            var smsCreator = new SMSCreator();
            smsCreator.SendNotification("Sending SMS");

        }
    }
}



public interface INotification
{
    public void SendMessage(string message);
}

public class SendEmail : INotification
{
    public void SendMessage(string message)
    {
        Console.WriteLine(message);
    }
}

public class SendSms : INotification
{
    public void SendMessage(string message)
    {
        Console.WriteLine(message);
    }
}

//FactoryMethod
public abstract class NotificationCreator
{
    public abstract INotification CreateNotification();

    public void SendNotification(string message)
    {
        var notification=CreateNotification();
        notification.SendMessage(message);
    }
}

public class EmailCreator : NotificationCreator
{
    public override INotification CreateNotification()
    {
        return new SendEmail();
    }
}

public class SMSCreator : NotificationCreator
{
    public override INotification CreateNotification()
    {
        return new SendSms();
    }
}