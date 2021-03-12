using System;

namespace UptoboxApi
{
    public static class Utils
    {
        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string SizeSuffix(long octets, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("invalid decimal places"); }
            if (octets < 0) { throw new Exception("Invalid Value"); }
            if (octets == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(octets, 1024);

            decimal adjustedSize = (decimal)octets / (int)Math.Pow(1000, mag);

            return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[mag]);
        }
    }
}
