namespace Starter
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IWeatherService weatherService = new WeatherService();
            IWeatherService weatherServiceProxy = new WeatherServiceProxy(weatherService);
           await weatherServiceProxy.GetTemperature("tehran");
           await weatherServiceProxy.GetTemperature("tehran");

        }
    }
}

public class DataClass
{
     public int Temperature { get;set; }
     public string City { get; set; }
}

public interface IWeatherService
{ 
    public Task<int> GetTemperature(string city);
}

public class WeatherService : IWeatherService
{
    public DataClass Data=new DataClass();

    public async Task<int> GetTemperature(string city)
    {
        Console.WriteLine("Loading Data...");
        await Task.Delay(1000);
        return Data.Temperature;
    }
}

public class WeatherServiceProxy : IWeatherService
{
    private Dictionary<string, int> _cache { get; set; } = new Dictionary<string, int>();
    private IWeatherService _weatherService;

    public WeatherServiceProxy(IWeatherService weatherService)
    {
        this._weatherService = weatherService;
    }

    public async Task<int> GetTemperature(string city)
    {
        if (_cache.TryGetValue(city,out var value))
        {
            return value;
        }
        
        var temperature = await this._weatherService.GetTemperature(city);
        _cache.Add(city, temperature);
        return temperature;
    }
}