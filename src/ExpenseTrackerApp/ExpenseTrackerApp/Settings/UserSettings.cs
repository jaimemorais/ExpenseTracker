using ExpenseTrackerApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ExpenseTrackerApp.Settings
{
    public class UserSettings : IUserSettings
    {
        private static string EMAIL = "email";
        private static string PWD = "pwd";
        private static string SHOW_PUPPY = "show_puppy";
        private static string CATEGORIES = "categories";
        private static string PAYMENTTYPES = "payment_types";



        public void SaveEmail(string email) => Preferences.Set(EMAIL, email);

        public string GetEmail() => Preferences.Get(EMAIL, string.Empty);


        public async Task SavePasswordAsync(string pwd) => await SecureStorage.SetAsync(PWD, pwd);

        public async Task<string> GetPasswordAsync()
        {
            try
            {
                return await SecureStorage.GetAsync(PWD);
            }
            catch
            {
                return null;
            }
        }



        public void SaveShowPuppyPref(bool value) => Preferences.Set(SHOW_PUPPY, value);

        public bool GetShowPuppyPref() => Preferences.Get(SHOW_PUPPY, true);



        public List<Category> GetCategoriesLocal()
        {
            string base64Data = Preferences.Get(CATEGORIES, null);
            if (base64Data != null)
                return (List<Category>)Base64ToObject(base64Data);
            return null;
        }

        public void SetCategoriesLocal(List<Category> categories) => Preferences.Set(CATEGORIES, ObjectToBase64String(categories));



        public List<PaymentType> GetPaymentTypesLocal()
        {
            string base64Data = Preferences.Get(PAYMENTTYPES, null);
            if (base64Data != null)
                return (List<PaymentType>)Base64ToObject(base64Data);
            return null;
        }

        public void SetPaymentTypesLocal(List<PaymentType> paymentTypes) => Preferences.Set(PAYMENTTYPES, ObjectToBase64String(paymentTypes));





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
