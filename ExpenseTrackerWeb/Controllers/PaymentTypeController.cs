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
    public class PaymentTypeController : BaseController
    {
        // GET: PaymentType
        public async Task<ActionResult> Index()
        {
            List<PaymentType> paymentTypes = await base.GetItemListAsync<PaymentType>("PaymentTypes");

            return View(paymentTypes);
        }


        // GET: PaymentType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaymentType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentType/Create
        [HttpPost]
        public async Task<ActionResult> Create(PaymentType paymentType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string url = base.GetApiServiceURL("PaymentTypes");

                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                    var response = await httpClient.PostAsJsonAsync(url, paymentType);

                    if (response.IsSuccessStatusCode)
                    {
                        ShowMessage("PaymentType '" + paymentType.Name + "' added.", EnumMessageType.INFO);
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
                ShowMessage("Error creating new paymentType.", EnumMessageType.ERROR);
                return View();
            }
        }

        // GET: PaymentType/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PaymentType paymentType = await base.GetItemByIdAsync<PaymentType>("PaymentTypes", id);

            if (paymentType == null)
            {
                return HttpNotFound();
            }

            return View(paymentType);
        }


        // POST: PaymentType/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string url = base.GetApiServiceURL("PaymentTypes");

                    PaymentType paymentTypePut = new PaymentType();
                    paymentTypePut.Name = collection["Name"];

                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                    var response = await httpClient.PutAsJsonAsync(url + "/" + id, paymentTypePut);

                    if (response.IsSuccessStatusCode)
                    {
                        ShowMessage("PaymentType '" + paymentTypePut.Name + "' updated.", EnumMessageType.INFO);
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
                ShowMessage("Error updating paymentType.", EnumMessageType.ERROR);
                return View();
            }
        }

        // GET: PaymentType/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PaymentType paymentType = await base.GetItemByIdAsync<PaymentType>("PaymentTypes", id);

            if (paymentType == null)
            {
                return HttpNotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentType/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(string id, FormCollection collection)
        {
            try
            {
                string url = base.GetApiServiceURL("PaymentTypes");

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");

                var response = await httpClient.DeleteAsync(url + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    ShowMessage("PaymentType deleted.", EnumMessageType.INFO);
                }
                else
                {
                    ShowMessage("Error contacting server.", EnumMessageType.ERROR);
                }

                return RedirectToAction("Index");

            }
            catch
            {
                ShowMessage("Error deleting paymentType.", EnumMessageType.ERROR);
                return View();
            }
        }
    }
}
