namespace Starter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PricingService pricingService = new PricingService();
            PaymentService paymentService = new PaymentService();
            NotificationService notificationService = new NotificationService();
            OrderService orderService=new OrderService();
            InventoryService inventoryService = new InventoryService();


            var facade = new OrderFacade(inventoryService, pricingService, paymentService, orderService,
                notificationService);
            facade.PlaceOrder("123",10,2);
            
        }
    }
}



public class InventoryService
{
    public bool IsAvailable(int productId, int quantity)
    {
        Console.WriteLine("Checking Inventory...");
        return true;
    }
}

public class PricingService
{
    public decimal CalculatePrice(int produtId, int quantity)
    {
        Console.WriteLine("Calculating Price...");
        return quantity * 100;
    }
}

public class PaymentService
{
    public bool Pay(string userId, decimal amount)
    {
        Console.WriteLine($"Processing payment for {userId},{amount}...");
        return true;
    }
}

public class NotificationService
{
    public void SendOrderConfirmation(string userId)
    {
        Console.WriteLine($"Sending order confirmation for {userId}");
    }
}

public class OrderService
{
    public void CreateOrder(string userId,int productId,int quantity)
    {
        Console.WriteLine($"Creating Order for user ${userId}");
    }
}

public class OrderFacade
{
    private readonly InventoryService _inventoryService;
    private readonly PricingService _pricingService;
    private readonly PaymentService _paymentService;
    private readonly OrderService _orderService;
    private readonly NotificationService _notificationService;

    public OrderFacade(InventoryService inventoryService, PricingService pricingService, PaymentService paymentService, OrderService orderService, NotificationService notificationService)
    {
        _inventoryService = inventoryService;
        _pricingService = pricingService;
        _paymentService = paymentService;
        _orderService = orderService;
        _notificationService = notificationService;
    }

    public void PlaceOrder(string userId, int productId, int quantity)
    {
        if (!_inventoryService.IsAvailable(productId,quantity))
        {
            throw new Exception("Not available");
        }

        var price = _pricingService.CalculatePrice(productId, quantity);
        if (!_paymentService.Pay(userId,price))
        {
            throw new Exception("Not paid");
        }
        
        _orderService.CreateOrder(userId, productId, quantity);
        _notificationService.SendOrderConfirmation(userId);
    }
}