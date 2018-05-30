using ExpenseTrackerApp.Helpers;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseTrackerApp.Utils
{
    public static class CryptoUtils
    {

        public static string EncryptStr(string input)
        {
            var aes = new RijndaelManaged
            {
                KeySize = 256,
                BlockSize = 256,
                Padding = PaddingMode.PKCS7,
                Key = Convert.FromBase64String(Secrets.CRYPTO_KEY),
                IV = Convert.FromBase64String(Secrets.CRYPTO_IV)
            };

            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(input);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            return Convert.ToBase64String(xBuff);            
        }


        public static string DecryptStr(string input)
        {
            RijndaelManaged aes = new RijndaelManaged
            {
                KeySize = 256,
                BlockSize = 256,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                Key = Convert.FromBase64String(Secrets.CRYPTO_KEY),
                IV = Convert.FromBase64String(Secrets.CRYPTO_IV)
            };

            var decrypt = aes.CreateDecryptor();
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(input);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            return Encoding.UTF8.GetString(xBuff);            
        }


    }
}
