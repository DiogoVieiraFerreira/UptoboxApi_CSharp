using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using UptoboxApi.Tools;

namespace UptoboxApi.Test
{
    public class ApiTest
    {
        private string _token = "";
        private string? _directoryProjectPath;

        [SetUp]
        public void Setup()
        {
            _token = MockFileConfig.Token;
            _directoryProjectPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
        }

        [Test]
        public async Task ContactUptobox()
        {
            const string uri = "/user/me";
            JObject? json = await Api.Get<JObject>(uri, _token);
            Assert.AreEqual(json?["token"].ToString(), _token);
        }

        [Test]
        public async Task UploadOneFile()
        {
            JObject? linkToUpload = await Api.Get<JObject>("/upload", _token);
            JObject? uploadedFile = await Api.Post<JObject>(
                $"https:{linkToUpload?["uploadLink"]}",
                Path.Join(_directoryProjectPath, "images", "cat.jpg")
            );

            const string actual = "cat.jpg";
            string? expected = uploadedFile?["files"]?[0]?["name"]?.ToString();

            Assert.AreEqual(expected, actual);
            Assert.IsTrue(!string.IsNullOrEmpty(uploadedFile?["files"]?[0]?["url"]?.ToString()));
            Assert.IsTrue(!string.IsNullOrEmpty(uploadedFile?["files"]?[0]?["deleteUrl"]?.ToString()));
        }

        [Test]
        public async Task UploadMultipleFiles()
        {
            JObject? linkToUpload = await Api.Get<JObject>("/upload", _token);
            JObject?[] uploadedFiles = await Api.Post<JObject>(
                $"https:{linkToUpload?["uploadLink"]}",
                new string[2]
                {
                    Path.Join(_directoryProjectPath, "images", "cat.jpg"),
                    Path.Join(_directoryProjectPath, "images", "dog_hello.gif")
                }
            );
            Assert.True(uploadedFiles.Length == 2);

            const string actual1 = "cat.jpg";
            string? expected1 = uploadedFiles?[0]?["files"]?[0]?["name"]?.ToString();

            Assert.AreEqual(expected1, actual1);
            Assert.IsTrue(!string.IsNullOrEmpty((string) uploadedFiles[0]?["files"]?[0]?["url"]));
            Assert.IsTrue(!string.IsNullOrEmpty((string) uploadedFiles[0]?["files"]?[0]?["deleteUrl"]));
            
            const string actual2 = "dog_hello.gif";
            string? expected2 = uploadedFiles?[1]?["files"]?[0]?["name"]?.ToString();

            Assert.AreEqual(expected2, actual2);
            Assert.IsTrue(!string.IsNullOrEmpty((string) uploadedFiles?[0]?["files"]?[0]?["url"]));
            Assert.IsTrue(!string.IsNullOrEmpty((string) uploadedFiles?[0]?["files"]?[0]?["deleteUrl"]));
        }

        [Test]
        public async Task SearchSpecificFile()
        {
            string nameOfFile = "cat.jpg";
            //upload the file to search it 
            JObject? linkToUpload = await Api.Get<JObject>("/upload", _token);
            JObject? uploadedFile = await Api.Post<JObject>(
                $"https:{linkToUpload?["uploadLink"]}",
                Path.Join(_directoryProjectPath, "images", nameOfFile)
            );
            //Search the file
            JObject? searchedObject = await Api.Get<JObject>("/user/files", _token, new QueryParameters()
            {
                SearchField = "file_name",
                Limit = 1,
                Search = nameOfFile,
                Path = @"//"
            });
            Assert.AreEqual(searchedObject?["files"]?[0]?["file_name"]?.ToString(), nameOfFile);
        }
    }
}