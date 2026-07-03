namespace Starter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var emp1 = new Employee
            {
                Address = new Address
                {
                    City = "Tehran",
                    Street = "Valiasr"
                },
                Name = "Ahmadi"
            };

            var emp2 = emp1.ShallowCopy();
            // emp2.Address.City = "Kashan";
            Console.WriteLine(emp1.Address.City);

            var emp3 = emp1.DeepCopy();
            emp3.Address.City = "Ahvaz";
            Console.WriteLine(emp1.Address.City);

            

        }
    }
}



public class Employee
{
    public string Name { get; set; }
    public Address Address { get; set; }

    public Employee ShallowCopy()
    {
        return (Employee)this.MemberwiseClone();
    }

    public Employee DeepCopy()
    {
        return new Employee
        {
            Name = this.Name,
            Address = new Address
            {
                City = this.Address.City,
                Street = this.Address.Street,
            }
        };
    }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
}
