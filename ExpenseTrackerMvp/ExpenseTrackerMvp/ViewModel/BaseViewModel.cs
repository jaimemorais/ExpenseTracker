using System;

namespace ExpenseTrackerMvp.ViewModel
{
    public class BaseViewModel
    {
        protected string GetApiServiceURL(string apiId)
        {
            try
            {
                return "http://10.0.2.2:50805/api/" + apiId;
            }
            catch
            {
                throw new Exception("WebApiServiceURL not set.");
            }
        }

    }
}
