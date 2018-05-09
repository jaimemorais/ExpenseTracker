using ExpenseTrackerApp.Model;
using ExpenseTrackerApp.Utils;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ExpenseTrackerApp.Settings
{
    public class UserSettings : IUserSettings
    {
        private static string EMAIL = "email";
        private static string PWD = "pwd";
        private static string SHOW_PUPPY = "show_puppy";
        private static string CATEGORIES = "categories";
        private static string PAYMENTTYPES = "payment_types";


        private static ISettings Settings => CrossSettings.Current;





        public void SaveEmail(string email) => Settings.AddOrUpdateValue(EMAIL, email);

        public string GetEmail() => Settings.GetValueOrDefault(EMAIL, string.Empty);


        public void SavePassword(string pwd) => Settings.AddOrUpdateValue(PWD, CryptoUtils.EncryptStr(pwd));

        public string GetPassword()
        {
            try
            {
                return CryptoUtils.DecryptStr(Settings.GetValueOrDefault(PWD, string.Empty));
            }
            catch
            {
                return null;
            }
        }



        public void SaveShowPuppyPref(bool value) => Settings.AddOrUpdateValue(SHOW_PUPPY, value);

        public bool GetShowPuppyPref() => Settings.GetValueOrDefault(SHOW_PUPPY, true);



        public List<Category> GetCategoriesLocal()
        {
            string base64Data = Settings.GetValueOrDefault(CATEGORIES, null);
            if (base64Data != null)
                return (List<Category>)Base64ToObject(base64Data);
            return null;
        }

        public void SetCategoriesLocal(List<Category> categories) => Settings.AddOrUpdateValue(CATEGORIES, ObjectToBase64String(categories));



        public List<PaymentType> GetPaymentTypesLocal()
        {
            string base64Data = Settings.GetValueOrDefault(PAYMENTTYPES, null);
            if (base64Data != null)
                return (List<PaymentType>)Base64ToObject(base64Data);
            return null;
        }

        public void SetPaymentTypesLocal(List<PaymentType> paymentTypes) => Settings.AddOrUpdateValue(PAYMENTTYPES, ObjectToBase64String(paymentTypes));





        private string ObjectToBase64String(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        private object Base64ToObject(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return new BinaryFormatter().Deserialize(ms);
            }
        }
    }
}
