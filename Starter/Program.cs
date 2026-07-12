namespace Starter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dev1 = new Employee("ali", 300);
            var dev2 = new Employee("vahid", 400);
            var dep1=new  Department("dep1");
            dep1.Add(dev1);
            dep1.Add(dev2);

            var manager = new Employee("Reza", 8000);
            var headquarters  = new Department("HQ");
            headquarters.Add(manager);
            headquarters.Add(dep1);
            Console.WriteLine(headquarters.GetSalary());
            
            ////////////////////////////////////////////////////////////////////////////////مثال۲
            var percentageDiscount = new PercentageDiscount(20);
            var flatDiscount = new FlatDiscount(50);

            var discountPipeLine = new DiscountPipeLine();
            discountPipeLine.Add(percentageDiscount);
            discountPipeLine.Add(flatDiscount);
            var total=discountPipeLine.Apply(1000);
            Console.WriteLine(total);
            
        }
    }
}


public interface IPayable
{
    public string Name { get; set; }
    public decimal GetSalary();
}

public class Employee : IPayable
{
    public Employee(string name,decimal salary)
    {
        Name = name;
        _salary = salary;
    }

    public string Name { get; set; }
    private readonly decimal _salary;
    public decimal GetSalary()
    {
        return _salary;
    }
}

public class Department : IPayable
{
    public Department(string name)
    {
        Name = name;
    }

    public List<IPayable> Employees =new  List<IPayable>();
    public string Name { get; set; }
    public decimal GetSalary()
    {
       return Employees.Sum(x => x.GetSalary());
    }

    public void Add(IPayable payable)
    {
        Employees.Add(payable);
    }
}



//مثال ۲

public interface IDiscount
{
    decimal Apply(decimal originalPrice);
}

public class PercentageDiscount : IDiscount
{
    public PercentageDiscount(decimal percentage)
    {
        Percentage = percentage;
    }

    public decimal Percentage { get; set; }
    
    public decimal Apply(decimal originalPrice)
    {
        return originalPrice - (originalPrice * Percentage / 100);
    }
}

public class FlatDiscount : IDiscount
{
    public FlatDiscount(decimal amount)
    {
        Amount = amount;
    }

    public decimal Amount{get;set;}
    public decimal Apply(decimal originalPrice)
    {
        return Math.Max(0, originalPrice - Amount);
    }
}

public class DiscountPipeLine:IDiscount
{
    private List<IDiscount> _Discounts = new();
    
    public void Add(IDiscount discount)=>_Discounts.Add(discount);
    public decimal Apply(decimal originalPrice)
    {
        decimal currentPrice = originalPrice;
        foreach (var discount in _Discounts)
        {
            currentPrice = discount.Apply(currentPrice);
        }
        return currentPrice;
    }
}