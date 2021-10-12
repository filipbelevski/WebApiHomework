using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Shared
{
    public static class HashStrings
    {
        public static string HashedString(string str)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(str));
            var hashedPasswordStr = Encoding.ASCII.GetString(md5data);

            return hashedPasswordStr;
        }
    }
}
