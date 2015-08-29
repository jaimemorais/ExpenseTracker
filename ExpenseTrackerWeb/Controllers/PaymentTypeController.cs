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
    public class PaymentTypeController : BaseController
    {
        // GET: PaymentType
        public async Task<ActionResult> Index()
        {
            List<PaymentType> paymentTypes = await base.GetPaymentTypesAsync();

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
                    string url = base.GetApiServiceURL("PaymentTypeApi");

                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                    var response = await httpClient.PostAsJsonAsync(url, paymentType);

                    if (response.IsSuccessStatusCode)
                    {
                        ShowMessage("Payment Type '" + paymentType.Name + "' added.", EnumMessageType.INFO);
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
                ShowMessage("Error creating new Payment Type.", EnumMessageType.ERROR);
                return View();
            }
        }

        // GET: PaymentType/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaymentType/Edit/5
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

        // GET: PaymentType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaymentType/Delete/5
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
