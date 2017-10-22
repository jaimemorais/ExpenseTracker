using ExpenseTrackerApp.Model;
using System.Collections.Generic;

namespace ExpenseTrackerApp.Models.Service
{
    public class PaymentTypeResponse : BaseResponse
    {

        public List<PaymentType> PaymentTypeList { get; set; }
    }
}
