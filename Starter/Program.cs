namespace Starter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("hiii");
            var builder = new UserBuilder();
            var user=builder.SetAddress("tehran").SetAge(20).SetUserName("vahid").Build();
            

        }
    }
}

public class User
{
    public string UserName { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
}


public class UserBuilder
{
    private readonly User _user;

    public UserBuilder()
    {
        _user = new User();
    }

    public UserBuilder SetUserName(string userName)
    {
        _user.UserName = userName;
        return this;
    }

    public UserBuilder SetAge(int age)
    {
        _user.Age = age;
        return this;
    }

    public UserBuilder SetAddress(string address)
    {
        _user.Address = address;
        return this;
    }

    public User Build()
    {
        return _user;
    }
}
