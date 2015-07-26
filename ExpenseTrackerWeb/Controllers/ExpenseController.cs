using ExpenseTrackerWeb.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            return View(exp);
        }


        private async Task GetCategorySelectListAsync()
        {
            List<SelectListItem> categoriesSelectList = new List<SelectListItem>();

            List<Category> categories = await GetCategoriesAsync();
            foreach (Category category in categories)
            {
                categoriesSelectList.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }

            ViewBag.Categories = categoriesSelectList;
        }



        // POST: Expense/Create
        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Expense expense, FormCollection form)
        {
            await GetCategorySelectListAsync();

            try
            {
                if (ModelState.IsValid)
                {

                    // TODO
                    //string selectedCategoryId = form["SelectedCategory"];
                    //expense.CategoryId = new ObjectId(selectedCategoryId);


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

                    return RedirectToAction("Create");
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Expense/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
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
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
