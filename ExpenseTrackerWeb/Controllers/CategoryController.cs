using ExpenseTrackerDomain.Models;
using ExpenseTrackerWeb.Filters;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ExpenseTrackerWeb.Controllers
{

    [AuthFilter]
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

                    category.UserName = Session["UserName"].ToString();

                    var response = await GetHttpClient().PostAsJsonAsync(url, category);

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
        public async Task<ActionResult> Edit(string id, Category categoryPut)
        {
            try
            {
                string url = base.GetApiServiceURL("Categories");

                categoryPut.Id = id;
                
                var response = await GetHttpClient().PutAsJsonAsync(url + "/" + id, categoryPut);

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
        public async Task<ActionResult> Delete(string id, FormCollection form)
        {
            try
            {
                string url = base.GetApiServiceURL("Categories");
                
                var response = await GetHttpClient().DeleteAsync(url + "/" + id);

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
