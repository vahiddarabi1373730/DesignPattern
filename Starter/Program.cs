namespace Starter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // INotifier notifier = new EmailNotifier();
            // notifier = new SmsNotifier(notifier);
            // notifier = new SlackNotifier(notifier);
            // notifier.Send("Server Is Down");
            
            
            //مثال۲
            //درخواست از بیرونی‌ترین لایه شروع می‌شود
            // تا داخلی‌ترین لایه می‌رود
            // بعد موقع برگشت هر لایه چیزی اضافه می‌کند
            // new SugarDecorator(
            //     new MilkDecorator(
            //         new SimpleCoffee()
            //     )
            // ).GetDescription();
            // SugarDecorator می‌پرسد داخل من چیست؟
            // داخلش MilkDecorator است
            //     MilkDecorator می‌پرسد داخل من چیست؟
            // داخلش SimpleCoffee است
            //     SimpleCoffee می‌گوید: "Simple Coffee"
            // MilkDecorator اضافه می‌کند: ", Milk"
            // SugarDecorator اضافه می‌کند: ", Sugar"
            ICoffee coffee = new SimpleCoffee();
            coffee = new MilkDecorator(coffee);
            coffee=new SugarDecorator(coffee);
            Console.WriteLine(coffee.GetDescription());
            Console.WriteLine(coffee.GetCost());
        }
    }
}


public interface INotifier
{
    public void Send(string message);
}

public class EmailNotifier : INotifier
{
    public void Send(string message)
    {
        Console.WriteLine($"Send Email: {message}");
    }
}

public class NotifierDecorator : INotifier
{
    private INotifier _notifier;

    public NotifierDecorator(INotifier notifier)
    {
        _notifier = notifier;
    }

    public virtual void Send(string message)
    {
        _notifier.Send(message);
    }
}

public class SmsNotifier : NotifierDecorator
{
    public SmsNotifier(INotifier notifier) : base(notifier)
    {
    }

    public override void Send(string message)
    {
        base.Send(message);
        Console.WriteLine($"Send Sms: {message}");
    }
}

public class SlackNotifier : NotifierDecorator
{
    public SlackNotifier(INotifier notifier):base(notifier){}

    public override void Send(string message)
    {
        base.Send(message);
        Console.WriteLine($"Send Slack: {message}");
    }
}





///////////////////////////////////////مثال۲


public interface ICoffee
{
    public string GetDescription();
    public int GetCost();
}

public class SimpleCoffee : ICoffee
{
    public string GetDescription()
    {
        return "Simple Coffee";
    }

    public int GetCost()
    {
        return 5;
    }
}

public class CoffeeDecorator : ICoffee
{
    private ICoffee _coffee;


    public CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public virtual string GetDescription()
    {
        return _coffee.GetDescription();
    }

    public virtual int GetCost()
    {
        return _coffee.GetCost();
    }
}

public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee){}

    public override string GetDescription()
    {
        return base.GetDescription()+" milk";
    }

    public override int GetCost()
    {
        return base.GetCost() + 2;
    }
}

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee){}

    public override string GetDescription()
    {
        return base.GetDescription()+" Sugar";
    }

    public override int GetCost()
    {
        return base.GetCost()+1;
    }
}