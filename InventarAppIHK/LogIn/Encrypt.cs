using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace InventarAppIHK
{
    public class Encrypt
    {
        /// <summary>
        /// Der StringBuilder stellt eine veränderbare Zeichenfolge dar.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashValue(string password)
        {
            StringBuilder stringBuilder= new StringBuilder();
            foreach(byte b in GetHashValue(password)) 
            {
                stringBuilder.Append(b.ToString("X3"));
            }
            return stringBuilder.ToString();
        }

        public static byte[] GetHashValue(string password)
        {
            using (HashAlgorithm Hashalgorithm= SHA256.Create()) 
                return Hashalgorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}










//https://www.youtube.com/watch?v=J3BbVzzfR3g       Hashwert
