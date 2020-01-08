using System;
using System.Text;

namespace FineCore.Lib {
    public static class MD5 {

        public static string GetMd5(string inputString) {

            var byteArray = Encoding.UTF8.GetBytes(inputString);

            var md5Array = System.Security.Cryptography.MD5.Create().ComputeHash(byteArray);

            StringBuilder sb = new StringBuilder();

            foreach (var b in md5Array) { sb.Append(b.ToString("x2")); }

            return sb.ToString();

        }

    }
}
