namespace Starter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IAbstractFactory factory = new PayPallFactory();
            factory.CreatePaymentService().Pay(1500);
        }
        
        
    }
}

public interface IPaymentService
{
    void Pay(decimal amount);
}

public interface IRefundService
{
    void Refund(string transactionId);
}

public interface IReportService
{
    void Report();
}


public class ZarinPalPaymentService : IPaymentService
{
    public void Pay(decimal amount)
    {
        Console.WriteLine("ZarinPalPaymentService Pay");
    }
}

public class ZarinPalRefundService : IRefundService
{
    public void Refund(string transactionId)
    {
        Console.WriteLine("ZarinPalRefundService Refund");
    }
}

public class ZarinPalReportService : IReportService
{
    public void Report()
    {
        Console.WriteLine("ZarinPalReportService Report");
    }
}



public class PayPalPaymentService : IPaymentService
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"PayPalPaymentService Pay {amount}");
    }
}

public class PayPalRefundService : IRefundService
{
    public void Refund(string transactionId)
    {
        Console.WriteLine("PayPalRefundService Refund");
    }
}

public class PayPalReportService : IReportService
{
    public void Report()
    {
        Console.WriteLine("PayPalReportService Report");
    }
}

//حالا یک interface می‌سازیم که می‌گوید هر Factory باید بتواند خانواده کامل سرویس‌ها را بسازد:

public interface IAbstractFactory
{
    IPaymentService CreatePaymentService();
    IRefundService CreateRefundService();
    IReportService CreateReportService();
}


public class ZarinPalFactory : IAbstractFactory
{
    public IPaymentService CreatePaymentService()
    {
        return new ZarinPalPaymentService();
    }

    public IRefundService CreateRefundService()
    {
        return new ZarinPalRefundService();
    }

    public IReportService CreateReportService()
    {
        return new ZarinPalReportService();
    }
}


public class PayPallFactory : IAbstractFactory
{
    public IPaymentService CreatePaymentService()
    {
        return new PayPalPaymentService();
    }

    public IRefundService CreateRefundService()
    {
        return new PayPalRefundService();
    }

    public IReportService CreateReportService()
    {
        return new PayPalReportService();
    }
}


