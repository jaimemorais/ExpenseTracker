using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace ExpenseTrackerMvp.Model
{
    public class Expense 
    {
        public String Id { get; set; }
     
        public string UserId { get; set; }

        
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime Date { get; set; }

        public double Value { get; set; }


        public string Category { get; set; }

        public string Description { get; set; }
                
        public string PaymentType { get; set; }

    }


    public class OnlyDateConverter : IsoDateTimeConverter
    {
        public OnlyDateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}