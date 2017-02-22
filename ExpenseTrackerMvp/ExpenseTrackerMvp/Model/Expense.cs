using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace ExpenseTrackerMvp.Model
{
    public class Expense : Base
    {

        private string id;
        public String Id
        {
            get { return id; }
            set
            {
                id = value;
                this.Notify();
            }
        }

        public string UserId { get; set; }


        private DateTime date;

        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime Date 
        {
            get { return date; }
            set
            {
                date = value;
                this.Notify();
            }
        }

        private double value;
        public double Value
        {
            get { return value; }
            set
            {
                this.value = value;
                this.Notify();
            }
        }


        private string category;
        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                this.Notify();
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                this.Notify();
            }
        }

        private string paymentType;
        public string PaymentType
        {
            get { return paymentType; }
            set
            {
                paymentType = value;
                this.Notify();
            }
        }

    }


    public class OnlyDateConverter : IsoDateTimeConverter
    {
        public OnlyDateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}