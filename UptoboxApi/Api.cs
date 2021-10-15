using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MimeTypes;
using Newtonsoft.Json.Linq;

namespace UptoboxApi
{
    /// <summary>
    /// Provides a base class to contact the Uptobox api
    /// </summary>
    public class Api {
        private static HttpClient? _client;
        private static HttpClient Client => _client ??= new HttpClient(/*new ConsoleLoggingHandler(new HttpClientHandler(), ConsoleLoggingHandler.LogginHandlerInformation.Request)*/)
        {
            DefaultRequestHeaders =
            {
                Accept =
                {
                    new MediaTypeWithQualityHeaderValue("application/json")
                }
            },
        };
        /// <summary>
        /// Send an asynchronously GET request to the Uptobox api
        /// </summary>
        /// <param name="path">api path (example: /user/files)</param>
        /// <param name="token">api access token,You can retrieve your API token by going to the page "My account" then click on the icon right after "token" to copy your token into the clipboard.</param>
        /// <param name="queryParameters">query parameters to use</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Generic Object with the values of the data field from the api result</returns>
        public static async Task<T?> Get<T>(string path, string token, QueryParameters? queryParameters = null)
        {
            if (path.Substring(0, 1) == "/") path = path.Substring(1);
            string requestUri = $"https://uptobox.com/api/{path}{GeneratePathParameters(queryParameters, token)}";
            HttpResponseMessage response = await Client.GetAsync(requestUri); 
            
            if (!response.IsSuccessStatusCode) return default(T);
            
            var content = await response.Content.ReadAsStringAsync();

            return JObject.Parse(content)["statusCode"].ToObject<int>() != 0
                ? default(T)
                : JObject.Parse(content)["data"].ToObject<T>();
        }
        
        /// <summary>
        /// Only used to upload elements at uptobox 
        /// </summary>
        /// <param name="uploadLink">gived by GET call at the next path "/upload"</param>
        /// <param name="filesPath">["C:\file_to_upload.txt", "C:\other_file.txt"]</param>
        /// <returns>Generic Array of the uploaded files data {name, size, url, deleteUrl}</returns>
        public static async Task<T[]> Post<T>(string uploadLink, string[] filesPath)
        {
            int arraySize = filesPath.Count();
            T[] uploadedFiles = new T[arraySize];
            
            for(int i = 0; i < arraySize; i ++)
            {
                uploadedFiles[i] = await Post<T>(uploadLink, filesPath[i]);
            }
            
            return uploadedFiles;
        }

        /// <summary>
        /// Only used to upload element at uptobox 
        /// </summary>
        /// <param name="uploadLink">gived by GET call at the next path "/upload"</param>
        /// <param name="filePath">"C:\file_to_upload.txt"</param>
        /// <returns>Generic of the uploaded file data {name, size, url, deleteUrl}</returns>
        public static async Task<T> Post<T>(string uploadLink, string filePath)
        {
            using (var multiForm = new MultipartFormDataContent())
            {
                FileStream fs = File.OpenRead(filePath);
                multiForm.Add(new StreamContent(fs), "file", Path.GetFileName(filePath));
                HttpResponseMessage response = await Client.PostAsync(uploadLink, multiForm);
                if (!response.IsSuccessStatusCode)
                {
                    return default(T);
                }
                string json = await response.Content.ReadAsStringAsync();
                
                return JObject.Parse(json).ToObject<T>();
            }
        }
        private static string GeneratePathParameters(QueryParameters? queryParameters, string token)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append($"?token={token}");
            
            if (queryParameters == null) return stringBuilder.ToString();
            
            stringBuilder.Append($"&file_code={queryParameters.FileCode}");
            stringBuilder.Append($"&limit={queryParameters.Limit}");
            stringBuilder.Append($"&offset={queryParameters.Offset}");
            stringBuilder.Append($"&dir={queryParameters.Dir().ToLower()}");
            stringBuilder.Append($"&orderBy={queryParameters.OrderBy}");
            stringBuilder.Append($"&path={Uri.EscapeDataString(queryParameters.Path)}");

            if(!string.IsNullOrEmpty(queryParameters.SearchField) && !string.IsNullOrEmpty(queryParameters.Search))
                stringBuilder.Append($"&searchField={queryParameters.SearchField}&search={Uri.EscapeDataString(queryParameters.Search)}");


            return stringBuilder.ToString();
        }
    }
}