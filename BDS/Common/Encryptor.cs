using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BDS.Common
{
    public static class Encryptor
    {
        public static string SHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();

            UTF8Encoding objUtf8 = new UTF8Encoding();
            byte[] hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));
            //StringBuilder hashResult = new StringBuilder();
            //for (int i = 0; i < str.Length; i++)
            //{
            //    hashResult.Append(hashValue[i].ToString());
            //}
            return Convert.ToBase64String(hashValue).ToString();
        }
    }
}