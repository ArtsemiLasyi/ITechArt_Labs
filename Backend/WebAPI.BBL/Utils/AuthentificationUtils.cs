using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utils
{
    public static class AuthentificationUtils
    {
        public static bool CompareHashes(byte[] first, byte[] second)
        {
            bool equal = false;
            if (first.Length == second.Length)
            {
                int i = 0;
                while ((i < first.Length) && (first[i] == second[i]))
                {
                    i += 1;
                }
                if (i == second.Length)
                {
                    equal = true;
                }
            }
            return equal;
        }

        public static byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider rncCsp = new RNGCryptoServiceProvider();
            byte[] salt = new byte[20];
            rncCsp.GetBytes(salt);

            return salt;
        }
    }
}
