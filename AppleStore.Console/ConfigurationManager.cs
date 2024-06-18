/*using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace AppleStore.Console
{
public static class ConfigurationManager
{
    private const string ConfigFileName = "application.json";
    private static AppConfig _config;

    public static async Task<AppConfig> LoadConfigAsync()
    {
        if (_config != null)
        {
            return _config;
        }

        try
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile configFile = await storageFolder.GetFileAsync(ConfigFileName);

            if (configFile == null)
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            string jsonText = await FileIO.ReadTextAsync(configFile);
            _config = JsonConvert.DeserializeObject<AppConfig>(jsonText);

            return _config;
        }
        catch (Exception ex)
        {
            throw new Exception("Error loading configuration", ex);
        }
    }

    public static async Task SaveConfigAsync(AppConfig config)
    {
        try
        {
            string jsonText = JsonConvert.SerializeObject(config, Formatting.Indented);
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile configFile = await storageFolder.CreateFileAsync(ConfigFileName, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(configFile, jsonText);
            _config = config;
        }
        catch (Exception ex)
        {
            throw new Exception("Error saving configuration", ex);
        }
    }
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; }
}

public class ApiSettings
{
    public string BaseUrl { get; set; }
}

public class AppSettings
{
    public string Theme { get; set; }
}

public class AppConfig
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public ApiSettings ApiSettings { get; set; }
    public AppSettings AppSettings { get; set; }
}

}*/