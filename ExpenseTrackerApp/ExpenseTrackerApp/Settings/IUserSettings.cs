using ExpenseTrackerApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Settings
{
    public interface IUserSettings
    {

        void SaveEmail(string email);
        Task SavePasswordAsync(string pwd);
        string GetEmail();
        Task<string> GetPasswordAsync();
        void SaveShowPuppyPref(bool value);
        bool GetShowPuppyPref();

        List<Category> GetCategoriesLocal();
        void SetCategoriesLocal(List<Category> categories);
        List<PaymentType> GetPaymentTypesLocal();
        void SetPaymentTypesLocal(List<PaymentType> paymentTypes);

    }
}
