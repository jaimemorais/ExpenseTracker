using ExpenseTrackerMvp.Util;
using System;

namespace ExpenseTrackerMvp.ViewModel
{
    public class BaseViewModel
    {
        protected string GetApiServiceURL(string apiId)
        {
            try
            {
                // 10.0.2.2 = localhost - emulator
                // IIS, not iisexpress 
                
                return AppConfig.Instance.GetExpenseTrackerApiUrl() + apiId;
            }
            catch
            {
                throw new Exception("expensetracker-api-url not set.");
            }
        }

    }
}
