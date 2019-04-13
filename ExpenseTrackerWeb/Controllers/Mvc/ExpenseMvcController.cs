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

    // TODO refactor, temporarily using session
    
    public class ExpenseMvcController : Controller
    {

        // http://localhost:59051/Mvc/ExpenseMvc?token=[token]&username=[username]
        public ActionResult Index(string token, string username)
        {
            CheckAuthSetSession(token, username);
            username = Session["username"].ToString();
            

            MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

            List<Expense> expenseList = expenseHelper.Collection
                .Find(e => e.UserName == username)
                .ToList()
                .OrderByDescending(e => e.Date)
                .ToList();

            return View("Index", expenseList);
        }


        private void CheckAuthSetSession(string token, string username)
        {
            if (Session["token"] == null || Session["username"] == null)
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

                Session["token"] = token;
                Session["username"] = username;
            }

        }


        public ActionResult NewExpense()
        {
            return View("NewExpense");
        }


        public ActionResult SaveExpense(Expense expense)
        {            
            try
            {
                CheckAuthSetSession(Session["token"].ToString(), Session["username"].ToString());


                MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

                expense.UserName = Session["username"].ToString();

                expenseHelper.Collection.InsertOneAsync(expense);

                return Index(Session["token"].ToString(), Session["username"].ToString());
            }
            catch (Exception e)
            {
                // TODO create user friendly response
                throw new Exception("ExpenseMvcController : Error saving expense : " + e.Message);
            }
        }
    }
}