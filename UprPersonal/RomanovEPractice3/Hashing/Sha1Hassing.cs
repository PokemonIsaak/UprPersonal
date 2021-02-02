using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RomanovEPractice3.Hashing
{
    class Sha1Hassing
    {
        private string _startSalt = "1kkjfu1!";
        private string _endSalt = "008384747&!wqehut,k@ii8!";

        public string HasString (string text)
        {
            var sb = new StringBuilder();

            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(_startSalt + text + _endSalt));                

                foreach (var b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
            }    

            return sb.ToString();
        }
    }
}
