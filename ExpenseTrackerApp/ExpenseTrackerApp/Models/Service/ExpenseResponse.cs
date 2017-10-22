using ExpenseTrackerApp.Model;
using System.Collections.Generic;

namespace ExpenseTrackerApp.Models.Service
{
    public class ExpenseResponse : BaseResponse
    {

        public List<Expense> ExpenseList { get; set; }
    }
}
