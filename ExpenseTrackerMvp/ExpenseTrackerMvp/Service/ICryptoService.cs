namespace ExpenseTrackerMvp.Service
{
    public interface ICryptoService
    {

        string EncryptString(string clearText, string encryptionKey);

        string DecryptString(string cipherText, string encryptionKey);

    }
}
