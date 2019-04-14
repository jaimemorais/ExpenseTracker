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
            TempCheckAuthSetSession(token, username);
            username = Session["username"].ToString();
            

            MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

            List<Expense> expenseList = expenseHelper.Collection
                .Find(e => e.UserName == username)
                .ToList()
                .Where(e => e.Date.CompareTo(DateTime.Now.AddDays(-40)) > 0) // only last 40 days
                .OrderByDescending(e => e.Date)                
                .ToList();

            return View("Index", expenseList);
        }


        // TODO refactor add login page/etc, temporarily using session
        private void TempCheckAuthSetSession(string token, string username)
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
            LoadDropDownLists();

            Expense exp = new Expense();
            exp.Date = DateTime.Today;

            return View("NewExpense", exp);
        }

        private void LoadDropDownLists()
        {
            List<Category> categoryList;
            if (Session["CategoryList"] == null)
            {
                MongoHelper<Category> categoryHelper = new MongoHelper<Category>();
                categoryList = categoryHelper.Collection.Find(e => e.UserName == Session["username"].ToString()).ToList();
                Session["CategoryList"] = categoryList;
            }
            else
            {
                categoryList = (List<Category>)Session["CategoryList"];
            }

            List<SelectListItem> catDropDownList = new List<SelectListItem>();
            foreach (Category cat in categoryList)
            {
                catDropDownList.Add(new SelectListItem() { Text = cat.Name, Value = cat.Name });
            }
            ViewBag.CategoryDropDownList = catDropDownList;


            List<PaymentType> paymentTypeList;
            if (Session["PaymentTypeList"] == null)
            {
                MongoHelper<PaymentType> paymentTypeHelper = new MongoHelper<PaymentType>();
                paymentTypeList = paymentTypeHelper.Collection.Find(e => e.UserName == Session["username"].ToString()).ToList();
                Session["PaymentTypeList"] = paymentTypeList;
            }
            else
            {
                paymentTypeList= (List<PaymentType>)Session["PaymentTypeList"];
            }

            List<SelectListItem> paymentTypeDropDownList = new List<SelectListItem>();
            foreach (PaymentType pt in paymentTypeList)
            {
                paymentTypeDropDownList.Add(new SelectListItem() { Text = pt.Name, Value = pt.Name });
            }
            ViewBag.PaymentTypeDropDownList = paymentTypeDropDownList;
        }



        public ActionResult SaveExpense(Expense expense)
        {            
            try
            {
                TempCheckAuthSetSession(Session["token"].ToString(), Session["username"].ToString());
                

                if (ModelState.IsValid)
                {
                    MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

                    expense.UserName = Session["username"].ToString();

                    expenseHelper.Collection.InsertOneAsync(expense);

                    return Index(Session["token"].ToString(), Session["username"].ToString());
                }
                else
                {
                    LoadDropDownLists();
                    return View("NewExpense", expense);
                }
            }
            catch (Exception e)
            {
                // TODO create user friendly response
                throw new Exception("ExpenseMvcController : Error saving expense : " + e.Message);
            }
        }


        public ActionResult DeleteExpense(string idExpenseToDelete)
        {
            try
            {
                TempCheckAuthSetSession(Session["token"].ToString(), Session["username"].ToString());
                
                MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

                var filter = Builders<Expense>.Filter.Eq(c => c.Id, idExpenseToDelete);                

                expenseHelper.Collection.DeleteOne(filter);

                return Index(Session["token"].ToString(), Session["username"].ToString());
            }
            catch (Exception e)
            {
                // TODO create user friendly response
                throw new Exception("ExpenseMvcController : Error deleting expense : " + e.Message);
            }

        }
    }

    
}