using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UptoboxApi;

namespace UptoboxApiTests
{
    [TestClass]
    public class FileTest
    {
        private string _name;
        private string _descr;
        private DateTime _created;
        private DateTime _lastDownload;
        private int _downloads;
        private string _fileCode;
        private string _url;
        private string _size;
        private string _password;
        private bool _public;
        private int _nbStream;
        private File _fileActual;
        [TestInitialize]
        public void Initialize()
        {
            _created = DateTime.Parse("2020-10-18 16:32:57");
            _lastDownload = DateTime.Parse("2021-03-13 14:00:10");
            _name = "Tree";
            _descr = "Amazon tree";
            _downloads = 2;
            _fileCode = "0a1b2c3d4e5f";
            _url = $"https://uptobox.com/{_fileCode}";
            _password = "";
            _public = true;
            _nbStream = 0;
            _size = "619.5 MB";

            _fileActual = new()
            {
                Created = _created,
                LastDownload = _lastDownload,
                Name = _name,
                Descr = _descr,
                Downloads = _downloads,
                Url = _fileCode,
                Password = _password,
                Public = _public,
                NbStream = _nbStream,
                Size = "619548670",
            };
        }

        [TestMethod]
        public void FileCreatedSuccess()
        {

            Assert.AreEqual(_created, _fileActual.Created);
            Assert.AreEqual(_lastDownload, _fileActual.LastDownload);
            Assert.AreEqual(_name, _fileActual.Name);
            Assert.AreEqual(_descr, _fileActual.Descr);
            Assert.AreEqual(_downloads, _fileActual.Downloads);
            Assert.AreEqual(_url, _fileActual.Url);
            Assert.AreEqual(_password, _fileActual.Password);
            Assert.AreEqual(_public, _fileActual.Public);
            Assert.AreEqual(_nbStream, _fileActual.NbStream);
            Assert.AreEqual(_size, _fileActual.Size);
        }
        [TestMethod]
        public void FileToStringSuccess()
        {
            string expectedName = $"{_name} ({_url})";
            Assert.AreEqual(expectedName, _fileActual.ToString());
        }
    }
}
