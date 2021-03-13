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
            _hash = "0123456789abcdef0123456789abcdef";
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
            _url = "https://uptobox.com/user_public?hash=0123456789abcdef&folder=1";

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
        [TestMethod]
        public void Root_Folder_With_Id_0_Not_Have_Url()
        {
            Folder folderActual = new()
            {
                Id = "0",
                Name = "Photos",
                Descr = "Album of holidays 2020",
                FullPath = "//Photos",
                Password = "",
                FileCount = 369,
                Hash = "0123456789abcdef0123456789abcdef",
                TotalFilesSize = "6904188494",
            };
            _url = "https://uptobox.com/user_public?hash=0123456789abcdef&folder=0";
            Assert.AreEqual("0", folderActual.Id);
            Assert.AreEqual(_name, folderActual.Name);
            Assert.AreEqual(_descr, folderActual.Descr);
            Assert.AreEqual(_fullPath, folderActual.FullPath);
            Assert.AreEqual(_password, folderActual.Password);
            Assert.AreEqual(_fileCount, folderActual.FileCount);
            Assert.AreEqual(_hash, folderActual.Hash);
            Assert.AreEqual("6.9 GB", folderActual.TotalFilesSize);

            Assert.AreNotEqual(_url, folderActual.Url);
        }
    }
}
