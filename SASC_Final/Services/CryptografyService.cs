using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SASC_Final.Services.Interfaces;

namespace SASC_Final.Services
{
    public class CryptografyService : ICryptografy
    {
        private string EncodeDecrypt(string str)
        {
            ushort secretKey = 0x0088; 
            var ch = str.ToArray(); 
            string newStr = "";      
            foreach (var c in ch) 
                newStr += TopSecret(c, secretKey);  
            return newStr;
        }

        private char TopSecret(char character, ushort secretKey)
        {
            character = (char)(character ^ secretKey);
            return character;
        }
        public string Decode(string cipher)
        {
            return EncodeDecrypt(cipher);
        }

        public string Encode(string plain)
        {
            return EncodeDecrypt(plain);
        }
    }
}
