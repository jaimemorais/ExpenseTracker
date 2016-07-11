using ExpenseTrackerDomain.Models;
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
    public class CategoryController : BaseController
    {
        // GET: Category
        public async Task<ActionResult> Index()
        {
            List<Category> categories = await base.GetItemListAsync<Category>("Categories");

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
                    string url = base.GetApiServiceURL("Categories");

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
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category cat = await base.GetItemByIdAsync<Category>("Categories", id);

            if (cat == null)
            {
                return HttpNotFound();
            }

            return View(cat);
        }
        

        // POST: Category/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string url = base.GetApiServiceURL("Categories");

                    Category categoryPut = new Category();
                    categoryPut.Name = collection["Name"];

                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                    var response = await httpClient.PutAsJsonAsync(url + "/" + id, categoryPut);

                    if (response.IsSuccessStatusCode)
                    {
                        ShowMessage("Category '" + categoryPut.Name + "' updated.", EnumMessageType.INFO);
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
                ShowMessage("Error updating category.", EnumMessageType.ERROR);
                return View();
            }
        }

        // GET: Category/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category cat = await base.GetItemByIdAsync<Category>("Categories", id);

            if (cat == null)
            {
                return HttpNotFound();
            }

            return View(cat);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, FormCollection collection)
        {
            try
            {
                string url = base.GetApiServiceURL("Categories");
                
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                                
                var response = await httpClient.DeleteAsync(url + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    ShowMessage("Category deleted.", EnumMessageType.INFO);
                }
                else
                {
                    ShowMessage("Error contacting server.", EnumMessageType.ERROR);
                }

                return RedirectToAction("Index");

            }
            catch
            {
                ShowMessage("Error deleting category.", EnumMessageType.ERROR);
                return View();
            }
        }
    }
}
