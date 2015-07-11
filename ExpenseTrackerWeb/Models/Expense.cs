using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTrackerWeb.Models
{
    public class Expense
    {
        [BsonId]
        public ObjectId ExpenseId { get; set; }

        public DateTime ExpenseDate { get; set; }
        public double Value { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }
    }
}