using System;

namespace UptoboxApi
{
        public class File
    {
        private string size;
        private string url;

        public string Name { get; set; }
        public string Descr { get; set; }
        public DateTime Created { get; set; }
        //set bytes and get string with real size (B, MB, GB, TB...)
        public string Size { get => size; set => size = Utils.SizeSuffix(long.Parse(value)); } //only on current folder data
        public int Downloads { get; set; }
        // url code used to download the file
        public string Url { get => url; set => url = $"https://uptobox.com/{value}"; }
        public string Password { get; set; }
        public bool Public { get; set; }
        public int NbStream { get; set; }
        public DateTime LastDownload { get; set; }

        public override string ToString() => $"{Name} ({url})";
    }
}
