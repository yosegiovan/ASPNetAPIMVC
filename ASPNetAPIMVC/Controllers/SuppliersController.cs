using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ASPNetAPIMVC.Controllers
{
    public class SuppliersController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44323/API/")
        };
        // GET: Suppliers
        public ActionResult Index()
        {
            IEnumerable<Supplier> suppliers = null;
            var respondTask = client.GetAsync("Suppliers");
            respondTask.Wait();
            var result = respondTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Supplier>>();
                readTask.Wait();
                suppliers = readTask.Result;
            }
            else
            {
                RedirectToAction("../Suppliers/Failed", new { sc = (int)result.StatusCode });
            }

            return View(suppliers);
        }

        public ActionResult Failed(int sc)
        {
            FailedModel fm = new FailedModel();
            fm.StatusCode = sc;
            return View(fm);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Supplier s)
        {
            //tdk usah savechanges, nt d lempar ke API
            HttpResponseMessage response = client.PostAsJsonAsync("Suppliers", s).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                RedirectToAction("../Suppliers/Failed", new { sc = (int)response.StatusCode });
            }

            //return View();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            IEnumerable<Supplier> suppliers = null;
            var respondTask = client.GetAsync("Suppliers");
            respondTask.Wait();

            var result = respondTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Supplier>>();
                readTask.Wait();
                suppliers = readTask.Result;
            }
            else
            {
                RedirectToAction("../Suppliers/Failed", new { sc = (int)result.StatusCode });
            }

            return View(suppliers.FirstOrDefault(s => s.SupplierID == id));
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<Supplier> suppliers = null;
            var respondTask = client.GetAsync("Suppliers");
            respondTask.Wait();

            var result = respondTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Supplier>>();
                readTask.Wait();
                suppliers = readTask.Result;
            }
            else
            {
                RedirectToAction("../Suppliers/Failed", new { sc = (int)result.StatusCode });
            }

            return View(suppliers.FirstOrDefault(s => s.SupplierID == id));
        }

        [HttpPost]
        public ActionResult Edit(Supplier s)
        {
            var deleteTask = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri("https://localhost:44323/API/Suppliers"),
                Content = new StringContent(JsonConvert.SerializeObject(s), Encoding.UTF8, "application/json")
            };

            var response = client.SendAsync(deleteTask);
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                RedirectToAction("../Suppliers/Failed", new { sc = (int)result.StatusCode });
            }

            return View();
        }

        public ActionResult Delete(int id)
        {
            IEnumerable<Supplier> suppliers = null;
            var respondTask = client.GetAsync("Suppliers");
            respondTask.Wait();

            var result = respondTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Supplier>>();
                readTask.Wait();
                suppliers = readTask.Result;
            }
            else
            {
                RedirectToAction("../Suppliers/Failed", new { sc = (int)result.StatusCode });
            }

            return View(suppliers.FirstOrDefault(s => s.SupplierID == id));
        }

        [HttpPost]
        public ActionResult Delete(Supplier s, int id)
        {
            Supplier sp = new Supplier();
            sp.SupplierID = id;
            sp.SupplierName = "";

            var deleteTask = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://localhost:44323/API/Suppliers"),
                Content = new StringContent(JsonConvert.SerializeObject(sp), Encoding.UTF8, "application/json")
            };

            var response = client.SendAsync(deleteTask);
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                RedirectToAction("../Suppliers/Failed", new { sc = (int)result.StatusCode });
            }

            return View();
        }

    }
}