using Microsoft.VisualStudio.TestTools.UnitTesting;
using UptoboxApi;

namespace UptoboxApiTests
{
    [TestClass]
    public class FolderTest
    {
        private string _id;
        private string _name;
        private string _descr;
        private string _fullPath;
        private string _password;
        private int _fileCount;
        private string _totalFilesSize;
        private string _hash;
        private string _url;
        [TestInitialize]
        public void Initialize()
        {
            _id = "1";
            _name = "Photos";
            _descr = "Album of holidays 2020";
            _fullPath = "//Photos";
            _password = "";
            _fileCount = 369;
            _totalFilesSize = "6904188494";
            _hash = "0123456789abcdef0123456789abcdef";
            _url = "https://uptobox.com/user_public?hash=0123456789abcdef&folder=1";
        }

        [TestMethod]
        public void FolderCreatedSuccess()
        {
            Folder folderActual = new()
            {
                Id = "1",
                Name = "Photos",
                Descr = "Album of holidays 2020",
                FullPath = "//Photos",
                Password = "",
                FileCount = 369,
                Hash = "0123456789abcdef0123456789abcdef",
                TotalFilesSize = "6904188494",
            };

            Assert.AreEqual(_id, folderActual.Id);
            Assert.AreEqual(_name, folderActual.Name);
            Assert.AreEqual(_descr, folderActual.Descr);
            Assert.AreEqual(_fullPath, folderActual.FullPath);
            Assert.AreEqual(_password, folderActual.Password);
            Assert.AreEqual(_fileCount, folderActual.FileCount);
            Assert.AreEqual(_hash, folderActual.Hash);
            Assert.AreEqual(_url, folderActual.Url);
            Assert.AreEqual("6.9 GB", folderActual.TotalFilesSize);
        }
    }
}
