using System.Configuration;

namespace UptoboxApi.Utils.FileConfig
{
    public class FileConfig : IFileConfig
    {
        public static string? Token => ConfigurationManager.AppSettings["token"];
    }

}