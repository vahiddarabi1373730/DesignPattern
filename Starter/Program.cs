namespace Starter
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            LegacyBankService legacyBankService=new  LegacyBankService();
            IPaymentGateway paymentGateway = new LegacyBankServiceAdapter(legacyBankService);
            CheckoutService checkoutService = new CheckoutService(paymentGateway);
            checkoutService.CheckoutAsync();

            LegacyUserSystem legacyUserSystem = new LegacyUserSystem();
            IUserService userService = new LegacyUserSystemAdapter(legacyUserSystem);
            await RunAsync(userService);
        }

        public static async Task RunAsync(IUserService userService)
        {
           var created= await userService.CreateUser(new CreateUserRequest
            {
                Email = "test",
                FirstName = "vahid",
                LastName = "darabi",
            });
            var user = await userService.GetUserByGuid(created.Guid);

            await userService.DeactivateAsync(created.Guid);

        }
    }
}


public interface IPaymentGateway
{
    public Task PayAsync(int amount);
}

public class LegacyBankService
{
    public void MakeTransaction(string amount)
    {
        Console.WriteLine($"{amount} made transaction");
    }
}

//مثال ۱

public class LegacyBankServiceAdapter:IPaymentGateway
{
    private LegacyBankService _legacyBankService;

    public LegacyBankServiceAdapter(LegacyBankService legacyBankService)
    {
        _legacyBankService = legacyBankService;
    }
    public Task PayAsync(int amount)
    {
        _legacyBankService.MakeTransaction(amount.ToString());
        return Task.CompletedTask;
    }
}


public class CheckoutService
{
    private IPaymentGateway _paymentGateway;

    public CheckoutService(IPaymentGateway paymentGateway)
    {
        _paymentGateway = paymentGateway;
    }

    public Task CheckoutAsync()
    {
        _paymentGateway.PayAsync(2500);
        return Task.CompletedTask;
    }
}



//مثال ۲

public class UserDto
{
    public Guid Guid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}

public class CreateUserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

public interface IUserService
{
     Task<UserDto?> GetUserByGuid(Guid guid);

     Task<UserDto> CreateUser(CreateUserRequest user);
     Task DeactivateAsync(Guid guid);
}

public class LegacyUserRecord
{
    public int Code { get; set; }
    public string FullName { get; set; } = "";
    public string Mail { get; set; } = "";
    public string Status { get; set; } = "";
}


public class LegacyUserSystem
{
    public List<LegacyUserRecord> _Users=new();

    public LegacyUserRecord? Find(int code)
    {
        var user = _Users.SingleOrDefault(u => u.Code == code);
        if (user == null)
        {
            return null;
        }
        return user;
    }
    public int Insert(string fullName,string email)
    {
        var nextCode=_Users.Count==0  ? 1 :_Users.Max(u=>u.Code)+1;
        var user = new LegacyUserRecord
        {
            Code = nextCode,
            Status = "D",
            FullName = fullName,
            Mail = email
        };
        _Users.Add(user);
        return nextCode;
    }
    public int DeactivateUser(int code)
    {
        var user=_Users.SingleOrDefault(u=>u.Code==code);
        if (user is null)
        {
            return -1;
        }

        user.Status = "D";
        return 1;
    } 
}

public class LegacyUserSystemAdapter : IUserService
{
    private LegacyUserSystem _legacyUserSystem;
    public LegacyUserSystemAdapter(LegacyUserSystem legacyUserSystem)
    {
        _legacyUserSystem=legacyUserSystem;
    }
    
    public Task<UserDto?> GetUserByGuid(Guid guid)
    {
        var legacyUserRecord = _legacyUserSystem.Find(ConvertGuidTiCode(guid));
        if (legacyUserRecord is null)
        {
            return Task.FromResult<UserDto?>(null);
        }

        var userDto = MapToUserDto(legacyUserRecord);
        return Task.FromResult<UserDto?>(userDto);

    }

    public Task<UserDto> CreateUser(CreateUserRequest user)
    {
        var fullName = user.FirstName + " " + user.LastName;
        var code = _legacyUserSystem.Insert(fullName, user.Email);
        
        
        var legacy=_legacyUserSystem.Find(code);
        var userDto = MapToUserDto(legacy!);
        return Task.FromResult<UserDto>(userDto);
    }

    public Task DeactivateAsync(Guid guid)
    {
        var result=_legacyUserSystem.DeactivateUser(ConvertGuidTiCode(guid));
        if (result==-1)
        {
            return Task.FromException<string>(new Exception("Get Error"));
        }
        
        return Task.CompletedTask;
    }

    private UserDto MapToUserDto(LegacyUserRecord legacyUserRecord)
    {
        return new UserDto()
        {
            Email = legacyUserRecord.Mail,
            FirstName = legacyUserRecord.FullName.Split(" ").FirstOrDefault()!,
            LastName = legacyUserRecord.FullName.Split(" ")[1],
            IsActive = legacyUserRecord.Status == "A",
            Guid = ConvertCodeToGuid(legacyUserRecord.Code),
        };
    }

    public static Guid ConvertCodeToGuid(int code)
    {
        var bytes = new byte[16];
        BitConverter.GetBytes(code).CopyTo(bytes,0);
        return new Guid(bytes);
    }

    public static int ConvertGuidTiCode(Guid guid)
    {
        var bytes = guid.ToByteArray();
        return BitConverter.ToInt32(bytes, 0);  
    }
}