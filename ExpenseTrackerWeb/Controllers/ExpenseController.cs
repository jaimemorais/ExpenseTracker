using ExpenseTrackerWeb.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTrackerWeb.Controllers
{
    public class ExpenseController : BaseController
    {
        // GET: Expense
        public ActionResult Index()
        {
            return View();
        }

        // GET: Expense/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Expense/Create
        public async Task<ActionResult> Create()
        {
            Expense exp = new Expense();
            exp.Date = DateTime.Now;

            await GetCategorySelectListAsync();
            await GetPaymentTypesSelectListAsync();

            return View(exp);
        }


        private async Task GetCategorySelectListAsync()
        {
            // TODO : cache

            List<SelectListItem> categoriesSelectList = new List<SelectListItem>();

            List<Category> categories = await base.GetItemListAsync<Category>("CategoryApi");
            foreach (Category category in categories)
            {
                categoriesSelectList.Add(new SelectListItem() { Text = category.Name, Value = category.Name });
            }

            ViewBag.Categories = categoriesSelectList;
        }

        private async Task GetPaymentTypesSelectListAsync()
        {
            // TODO : cache

            List<SelectListItem> paymentTypesSelectList = new List<SelectListItem>();

            List<PaymentType> paymentTypes = await base.GetItemListAsync<PaymentType>("PaymentTypeApi");
            foreach (PaymentType paymentType in paymentTypes)
            {
                paymentTypesSelectList.Add(new SelectListItem() { Text = paymentType.Name, Value = paymentType.Name });
            }

            ViewBag.PaymentTypes = paymentTypesSelectList;
        }



        // POST: Expense/Create
        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Expense expense, FormCollection form)
        {
            await GetCategorySelectListAsync();
            await GetPaymentTypesSelectListAsync();
            
            try
            {
                if (ModelState.IsValid)
                {
                    string url = base.GetApiServiceURL("ExpenseApi");
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                    var response = await httpClient.PostAsJsonAsync(url, expense);

                    if (response.IsSuccessStatusCode)
                    {
                        ShowMessage("Expense added.", EnumMessageType.INFO);
                    }
                    else
                    {
                        ShowMessage("Expense Create : Server error.", EnumMessageType.ERROR);
                    }

                    return RedirectToAction("Index", "Balance");
                }
                else
                {
                    return View();
                }
                                
                
            }
            catch
            {
                ShowMessage("Error creating new expense.", EnumMessageType.ERROR);
                return View();
            }
        }

        // GET: Expense/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = await GetExpenseById(id);

            if (expense == null)
            {
                return HttpNotFound();
            }
            
            return View(expense);
        }

        private async Task<Expense> GetExpenseById(string id)
        {
            string url = base.GetApiServiceURL("ExpenseApi");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            var response = await httpClient.GetAsync(id);

            // TODO Expense expense = response.Content;           

            return null;
        }


        // POST: Expense/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Expense expense, FormCollection collection)
        {
            await GetCategorySelectListAsync();            
            await GetPaymentTypesSelectListAsync();

            try
            {
                if (ModelState.IsValid)
                {
                    string url = base.GetApiServiceURL("ExpenseApi");
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                    var response = await httpClient.PutAsJsonAsync(url, expense);

                    if (response.IsSuccessStatusCode)
                    {
                        ShowMessage("Expense changed.", EnumMessageType.INFO);
                    }
                    else
                    {
                        ShowMessage("Expense Edit : Server error.", EnumMessageType.ERROR);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                ShowMessage("Error changing expense.", EnumMessageType.ERROR);
                return View();
            }
        }

        // GET: Expense/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Expense/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Expense expense, FormCollection collection)
        {
            await GetCategorySelectListAsync();
            await GetPaymentTypesSelectListAsync();

            try
            {
                if (ModelState.IsValid)
                {
                    string url = base.GetApiServiceURL("ExpenseApi");
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                    var response = await httpClient.DeleteAsync(url);//TODO expense                  


                    if (response.IsSuccessStatusCode)
                    {
                        ShowMessage("Expense deleted.", EnumMessageType.INFO);
                    }
                    else
                    {
                        ShowMessage("Expense Delete : Server error.", EnumMessageType.ERROR);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                ShowMessage("Error deleting expense.", EnumMessageType.ERROR);
                return View();
            }
        }
    }
}
