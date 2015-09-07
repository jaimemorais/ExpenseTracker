using ExpenseTrackerDomain.Models;
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
    public class CategoryController : BaseController
    {
        // GET: Category
        public async Task<ActionResult> Index()
        {
            List<Category> categories = await base.GetItemListAsync<Category>("CategoryApi");

            return View(categories);
        }


        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string url = base.GetApiServiceURL("CategoryApi");

                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                    var response = await httpClient.PostAsJsonAsync(url, category);

                    if (response.IsSuccessStatusCode)
                    {
                        ShowMessage("Category '" + category.Name + "' added.", EnumMessageType.INFO);
                    }
                    else
                    {
                        ShowMessage("Error contacting server.", EnumMessageType.ERROR);
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
                ShowMessage("Error creating new category.", EnumMessageType.ERROR);
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Category/Edit/5
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

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Category/Delete/5
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
