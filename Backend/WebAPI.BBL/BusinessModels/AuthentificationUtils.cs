using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BBL.BusinessModels
{
    public static class AuthentificationUtils
    {
        public static bool CompareHashes(byte[] first, byte[] second)
        {
            bool bEqual = false;
            if (first.Length == second.Length)
            {
                int i = 0;
                bool compareCondition = (i < first.Length) && (first[i] == second[i]);
                while (compareCondition)
                {
                    i += 1;
                }
                if (i == second.Length)
                {
                    bEqual = true;
                }
            }
            return bEqual;
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
