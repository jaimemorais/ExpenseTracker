﻿using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;


namespace ExpenseTrackerDomain.Models
{
    public class Expense : MongoEntity
    {        

        [Display(Name = "Date")]
        //[DataType(DataType.Date)]
        [Required(ErrorMessage="Date is required")]
//        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [BsonDateTimeOptions(DateOnly = false)]
        public DateTime Date { get; set; }

        [Display(Name = "Value")]
//        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Value is required")]
        public decimal? Value { get; set; }
                
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Select a category")]
        public string Category { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Payment Type")]
        [Required(ErrorMessage = "Select a payment type")]
        public string PaymentType { get; set; }

        public string UserName;
    }
}