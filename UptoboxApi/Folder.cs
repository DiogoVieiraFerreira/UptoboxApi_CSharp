using System;
using System.Collections.Generic;

namespace UptoboxApi
{
    public class Folder
    {
        private const int _hashLength = 16;
        private string _totalFilesSize;
        public List<File> Files { get; set; }
        public List<Folder> Folders { get; set; }
        public string Id{ get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
        public string FullPath { get; set; }
        public string Password { get; set; }
        public int FileCount { get; set; } //only on current folder data
        public string TotalFilesSize { get => _totalFilesSize; set => _totalFilesSize = Utils.SizeSuffix(long.Parse(value)); } //only on current folder data
        public string Hash { get; set; }
        public string Url
        {
            get => Id == "0" ? "" : $"https://uptobox.com/user_public?hash={Hash.Substring(_hashLength)}&folder={Id}";
        }

        public override string ToString() =>  $"{Name ?? "/"} ({FileCount}) : {Url}";
    }
}