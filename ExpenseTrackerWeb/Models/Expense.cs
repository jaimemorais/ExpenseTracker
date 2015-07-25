using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ExpenseTrackerWeb.Models
{
    public class Expense
    {
        
        [BsonId]
        public ObjectId ExpenseId { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage="Date is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Value")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Value is required")]
        public double Value { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Select a category")]
        public string Category { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "PaymentType")]
        [Required(ErrorMessage = "Select a payment type")]
        public string PaymentType { get; set; }
    }
}