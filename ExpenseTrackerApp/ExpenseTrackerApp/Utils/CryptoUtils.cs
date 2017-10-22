using PCLCrypto;
using System;
using System.Text;

namespace ExpenseTrackerApp.Utils
{
    public static class CryptoUtils
    {

        readonly static byte[] SALT = new byte[] { 1, 6, 9, 8, 1, 0, 7, 8 };        

        public static string EncryptStr(string str)
        {
            byte[] key = NetFxCrypto.DeriveBytes.GetBytes(AppSettings.CRYPTO_PASSWORD, SALT, 1000, 32);

            ISymmetricKeyAlgorithmProvider aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            ICryptographicKey symmKey = aes.CreateSymmetricKey(key);
            var encryptedBytes = WinRTCrypto.CryptographicEngine.Encrypt(symmKey, Encoding.UTF8.GetBytes(str));

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string DecryptStr(string str)
        {
            byte[] key = NetFxCrypto.DeriveBytes.GetBytes(AppSettings.CRYPTO_PASSWORD, SALT, 1000, 32);

            byte[] bytesToDecrypt = Convert.FromBase64String(str);

            ISymmetricKeyAlgorithmProvider aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            ICryptographicKey symmKey = aes.CreateSymmetricKey(key);
            var bytes = WinRTCrypto.CryptographicEngine.Decrypt(symmKey, bytesToDecrypt);

            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }
    }
}
