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
                
                return "http://10.0.2.2:80/expensetrackerapi/api/" + apiId;
            }
            catch
            {
                throw new Exception("WebApiServiceURL not set.");
            }
        }

    }
}
