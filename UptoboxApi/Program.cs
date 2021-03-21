using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CommandLine;
using Newtonsoft.Json.Linq;

namespace UptoboxApi
{
    class Program
    {
        public class Options
        {
            [Option('s', "search", Required = false, HelpText = "show the specific folder on your uptobox")]
            public string Search { get; set; }
            [Option('c', "count", Required = false, HelpText = "show the number of files in your folder")]
            public bool Count { get; set; }
            [Option('u', "url", Required = false, HelpText = "get the url of the folder")]
            public bool Url { get; set; }
            [Option('l', "link", Required = false, HelpText = "get links of files")]
            public bool Link { get; set; }
            [Option('r', "recursive", Required = false, HelpText = "get all files and subfolders of folder")]
            public bool Recursive { get; set; }
            [Option('m', "move", Required = false, HelpText = "move a file or folder into another location")]
            public string Move { get; set; }
            [Option('d', "delete", Required = false, HelpText = "delete a file or folder")]
            public string Delete { get; set; }
            [Option('t', "token", Required = true, HelpText = "your uptobox token")]
            public string Token { get; set; }
            
        }
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
            });
        }
    }
    public class Api {
        private static HttpClient _client;
        private static string _url;

        public Api(string baseUrl, string token){
            _url = $"api/user/files?token={token}&limit=100";
            _client = new HttpClient {BaseAddress = new Uri(baseUrl)};
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        //todo create filters
        public static async Task<T> Get<T>(string path){
            HttpResponseMessage response = await _client.GetAsync($"{_url}&orderBy=file_name&dir=asc&path={Uri.EscapeDataString(path)}"); // TODO encode URI
            if (!response.IsSuccessStatusCode) return default(T);
            
            var content = await response.Content.ReadAsStringAsync();
            
            return JObject.Parse(content)["statusCode"].ToObject<int>() != 0 ? default(T) : JObject.Parse(content)["data"].ToObject<T>();
        }
    }
}
