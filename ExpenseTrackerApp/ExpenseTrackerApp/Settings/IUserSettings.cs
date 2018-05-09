using ExpenseTrackerApp.Model;
using System.Collections.Generic;

namespace ExpenseTrackerApp.Settings
{
    public interface IUserSettings
    {

        void SaveEmail(string email);
        void SavePassword(string pwd);
        string GetEmail();
        string GetPassword();
        void SaveShowPuppyPref(bool value);
        bool GetShowPuppyPref();

        List<Category> GetCategoriesLocal();
        void SetCategoriesLocal(List<Category> categories);
        List<PaymentType> GetPaymentTypesLocal();
        void SetPaymentTypesLocal(List<PaymentType> paymentTypes);

    }
}
