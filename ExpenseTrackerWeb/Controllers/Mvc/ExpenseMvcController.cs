using ExpenseTrackerDomain.Models;
using ExpenseTrackerWebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using System.Configuration;
using System.Web.Http;
using System.Net;

namespace ExpenseTrackerWebApi.Controllers.Mvc
{
    public class ExpenseMvcController : Controller
    {

        // http://localhost:59051/Mvc/ExpenseMvc?token=[token]&username=[username]
        public ActionResult Index(string token, string username)
        {
            if (token == null || token != ConfigurationManager.AppSettings.Get("expensetracker-api-token"))
            {
                // TODO create user friendly response
                throw new Exception("ExpenseMvcController : wrong token or not informed");
            }
            
            if (string.IsNullOrEmpty(username))
            {
                // TODO create user friendly response
                throw new Exception("ExpenseMvcController : username not informed");
            }

            MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

            List<Expense> expenseList = expenseHelper.Collection
                .Find(e => e.UserName == username)
                .ToList()
                .OrderByDescending(e => e.Date)
                .ToList();
            
            return View(expenseList);
        }

        public ActionResult NewExpense()
        {
            return View("NewExpense");
        }


        public ActionResult SaveExpense(string token, string username, Expense expense)
        {
            
            /* TODO
            if (UtilApi.GetHeaderValue(Request, "expensetracker-api-token") == null ||
                UtilApi.GetHeaderValue(Request, "expensetracker-api-token") != ConfigurationManager.AppSettings.Get("expensetracker-api-token"))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            } */


            MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

            try
            {
                expenseHelper.Collection.InsertOneAsync(expense);

                return Index(token, username);
            }
            catch (Exception e)
            {
                // TODO create user friendly response
                throw new Exception("ExpenseMvcController : Error saving expense : " + e.Message);
            }
        }
    }
}