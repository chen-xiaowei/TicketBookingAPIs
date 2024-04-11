using Microsoft.Extensions.Configuration.Json;

namespace TicketBookingAPIs.Config
{
    public class AppSettingsJson
    {
        public static IConfiguration Configuration { get; set; }
        static AppSettingsJson()
        {
            Configuration = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
                .Build();
        }

        public static string? SecretKey 
        {
            get 
            { 
                var key = AppSettingsJson.Configuration.GetSection("SecretKey").Value;
                if (string.IsNullOrEmpty(key))
                    throw new ApplicationException("SecretKey is missing.");
                return key;
            } }
    }
}
