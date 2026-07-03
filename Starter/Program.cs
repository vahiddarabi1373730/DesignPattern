namespace Starter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configForPayment = ConfigurationManager.Instance; 
            //چون ConfigurationManager.Instance یک آبجکت برمیگرداند میتوان از GetSetting که non-static است دسترسی داشت
            string apiKey = configForPayment.GetSetting("ApiKey");
            
            
            //این جا دیگر سازنده یا constructor صدا زده نمیشود.چون آبجکت آن از قبل ساخته شده است
            var configForDb = ConfigurationManager.Instance;
            string dbConn = configForDb.GetSetting("DbConnectionString");
        }
    }
}


public sealed class ConfigurationManager
{
    
    public static readonly Lazy<ConfigurationManager> _instance=new Lazy<ConfigurationManager>(()=>new ConfigurationManager());
    
    private Dictionary<string,string> _settings=new Dictionary<string, string>();
    private ConfigurationManager()
    {
        _settings.Add("port","4200");
        _settings.Add("host","localhost");
    }

    public static ConfigurationManager Instance=>_instance.Value;
    public string GetSetting(string key)
    {
        if (_settings.TryGetValue(key,out var value))
        {
            return value;
        }
        return string.Empty;

    }
}

