using System;

namespace ExpenseTrackerApp.Model
{
    public class Expense 
    {
        public String Id { get; set; }
     
        public string UserName { get; set; }
                
        public DateTime Date { get; set; }

        public double Value { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }
                
        public string PaymentType { get; set; }

    }
    
}