using DVLD_Bisnesess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Buisness.Global
{
    public class clsGlobal
    {
        public static string ComputeHash(string Input)
        {
            // SHA is security hash algorithm
            // create as instance of the SHA-256 algorithm
            using (SHA256 sHA256 = SHA256.Create())
            {
                // compute the hash value from UTF8 encoded input string 
                byte[] hashBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(Input));

                // Convert the byte array to lowercase hexadecimal string 
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
