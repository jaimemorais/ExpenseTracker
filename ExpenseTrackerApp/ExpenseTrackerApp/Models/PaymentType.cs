using System;

namespace ExpenseTrackerApp.Model
{

    [Serializable]
    public class PaymentType
    {    
        public string UserName { get; set; }        

        public string Name { get; set; }
    }

}