using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using CommandLine;
using Newtonsoft.Json.Linq;
using UptoboxApi.Utils.FileConfig;

namespace UptoboxApi
{
    internal class Arguments
    {
        [Option('t', "token", Required = false, HelpText = "your Uptobox access token")]
        public string? Token { get; set; }

        [Option("save", Required = false, HelpText = "Save your token in the Config file")]
        public bool Save { get; set; }
        [Option('s', "search", Required = false, HelpText = "wich file do you search ?")]
        public string? Search { get; set; }
        [Option('f', "searchField", Required = false, HelpText = "define the field to be used for the search (default: file_name)")]
        public string? SearchField { get; set; }
        [Option('p', "path", Required = false, HelpText = "Where is the file do you search ? (ex: //FTP, default: //) or define the path for the upload")]
        public string? Path { get; set; }
        [Option('u', "upload", Required = false, HelpText = "path of the file to upload")]
        public string? Upload { get; set; }
        
    }

    internal class Program
    {
        private static string _token = "";

        static async Task Main(string[] args)
        {
            QueryParameters queryParameters = new();
            Console.WriteLine(FileConfig.Token);
            Parser.Default.ParseArguments<Arguments>(args).WithParsed((arguments) =>
            {
                _token = arguments.Token ?? FileConfig.Token ?? string.Empty;
                
                if(string.IsNullOrEmpty(_token)) return;
                if (arguments.Save) SaveToken(_token);
                if (!string.IsNullOrEmpty(arguments.Search))
                    queryParameters.Search = arguments.Search;
                if (!string.IsNullOrEmpty(arguments.SearchField))
                    queryParameters.SearchField = arguments.SearchField;
                else if(!string.IsNullOrEmpty(arguments.Search) && string.IsNullOrEmpty(arguments.SearchField))
                    queryParameters.SearchField = "file_name";
                if (!string.IsNullOrEmpty(arguments.Path))
                    queryParameters.Path = arguments.Path;
            });
            Console.WriteLine(queryParameters);
            var a = await Api.Get<JObject>("/upload", _token);
            // Console.WriteLine(a["uploadLink"]);
            var b = await Api.Post<JObject>(
                $"https:{a["uploadLink"]}",
                new string[2]
                {
                    @"C:\Users\noxcaedibux\OneDrive\Images\cat meme.jpg",
                    @"C:\Users\noxcaedibux\OneDrive\Images\crepeuh.png",
                });
            Console.WriteLine(b[0]["files"][0]["name"]);
            // get attributs to detect if it's a dir an upload all files inside it...
            // FileAttributes fileAttributes = File.GetAttributes(filePath);
            // if (fileAttributes.HasFlag(FileAttributes.Directory))
            //     return await Post<T>(uploadLink, Directory.GetFiles(filePath));
        }
        private static void SaveToken(string token)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Clear();
            config.AppSettings.Settings.Add("token",token );
            config.Save(ConfigurationSaveMode.Full);
        }
    }
}
