using ExpenseTrackerApp.Model;
using System.Collections.Generic;

namespace ExpenseTrackerApp.Models.Service
{
    public class CategoryResponse : BaseResponse
    {

        public List<Category> CategoryList { get; set; }
    }
}
