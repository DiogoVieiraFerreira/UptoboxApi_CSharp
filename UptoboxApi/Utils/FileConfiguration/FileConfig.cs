using System.Configuration;

namespace UptoboxApi.Utils.FileConfiguration
{
    public class FileConfig : IFileConfig
    {
        public static string? Token => ConfigurationManager.AppSettings["token"];
    }

}