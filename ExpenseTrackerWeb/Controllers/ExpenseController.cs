using ExpenseTrackerDomain.Models;
using ExpenseTrackerWeb.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ExpenseTrackerWeb.Controllers
{

    [AuthFilter]
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
            ViewBag.dateInit = DateTime.Now.ToString("dd/MM/yyyy");

            await GetCategorySelectListAsync();
            await GetPaymentTypesSelectListAsync();

            return View(exp);
        }


        private async Task GetCategorySelectListAsync()
        {
            // TODO : cache

            List<SelectListItem> categoriesSelectList = new List<SelectListItem>();
             
            List<Category> categories = await base.GetItemListAsync<Category>("Categories");
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
             
            List<PaymentType> paymentTypes = await base.GetItemListAsync<PaymentType>("PaymentTypes");
            foreach (PaymentType paymentType in paymentTypes)
            {
                paymentTypesSelectList.Add(new SelectListItem() { Text = paymentType.Name, Value = paymentType.Name });
            }

            ViewBag.PaymentTypes = paymentTypesSelectList;
        }



        // POST: Expense/Create
        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Expense expense)
        {
            await GetCategorySelectListAsync();
            await GetPaymentTypesSelectListAsync();
            
            try
            {
                if (ModelState.IsValid)
                {
                    string url = base.GetApiServiceURL("Expenses");
                    
                    expense.UserName = Session["UserName"].ToString();

                    var response = await GetHttpClient().PostAsJsonAsync(url, expense);

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
            
            Expense expense = await base.GetItemByIdAsync<Expense>("Expenses", id);

            if (expense == null)
            {
                return HttpNotFound();
            }

            await GetCategorySelectListAsync();
            await GetPaymentTypesSelectListAsync();

            return View(expense);
        }
        


        // POST: Expense/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, Expense expensePut)
        {           
            try
            {
                string url = base.GetApiServiceURL("Expenses");

                expensePut.Id = id;

                expensePut.UserName = Session["UserName"].ToString();


                var response = await GetHttpClient().PutAsJsonAsync(url + "/" + id, expensePut);

                if (response.IsSuccessStatusCode)
                {
                    ShowMessage("Expense updated.", EnumMessageType.INFO);
                }
                else
                {
                    await GetCategorySelectListAsync();
                    await GetPaymentTypesSelectListAsync();

                    ShowMessage("Expense Edit : Server error.", EnumMessageType.ERROR);
                }

                return RedirectToAction("Index", "Balance");

            }
            catch
            {
                ShowMessage("Error updating expense.", EnumMessageType.ERROR);
                return View();
            }
        }

        // GET: Expense/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Expense exp = await base.GetItemByIdAsync<Expense>("Expenses", id);

            if (exp == null)
            {
                return HttpNotFound();
            }

            return View(exp);
        }

        // POST: Expense/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, FormCollection form)
        {
            try
            {

                string url = base.GetApiServiceURL("Expenses");
                
                var response = await GetHttpClient().DeleteAsync(url + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    ShowMessage("Expense deleted.", EnumMessageType.INFO);
                }
                else
                {
                    await GetCategorySelectListAsync();
                    await GetPaymentTypesSelectListAsync();

                    ShowMessage("Expense Delete : Server error.", EnumMessageType.ERROR);
                }
                
                return RedirectToAction("Index", "Balance");
            }
            catch
            {
                ShowMessage("Error deleting expense.", EnumMessageType.ERROR);
                return View();
            }
        }
    }
}
