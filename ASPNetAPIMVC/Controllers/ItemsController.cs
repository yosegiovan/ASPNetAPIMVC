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
    public class ItemsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44323/API/")
        };
        // GET: Items
        public ActionResult Index()
        {
            IEnumerable<Item> items = PopulateItems();
            IEnumerable<Supplier> suppliers = PopulateSuppliers();

            var Record = from i in items
                         join s in suppliers on i.SupplierID equals s.SupplierID into table1
                         from s in table1.DefaultIfEmpty()
                         select new IndexItemModel
                         {
                             item_details = i,
                             supplier_details = s
                         };

            return View(Record);
        }

        public ActionResult Create()
        {
            PopulateSuppliersDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateItemModel cm)
        {
            Item item = new Item();
            item.ItemName = cm.ItemName;
            item.Price = cm.Price;
            item.Quantity = cm.Quantity;
            item.SupplierID = cm.SupplierID;

            HttpResponseMessage response = client.PostAsJsonAsync("Items", item).Result;

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

        public ActionResult Edit(int id)
        {
            IEnumerable<Item> items = PopulateItems();
            var item = items.FirstOrDefault(i => i.ItemID == id);
            PopulateSuppliersDropDownList(item);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Item i)
        {
            var editTask = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri("https://localhost:44323/API/Items"),
                Content = new StringContent(JsonConvert.SerializeObject(i), Encoding.UTF8, "application/json")
            };

            var response = client.SendAsync(editTask);
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

        public ActionResult Details(int id)
        {
            IEnumerable<Item> items = PopulateItems();
            IEnumerable<Supplier> suppliers = PopulateSuppliers();

            var Record = from i in items
                         join s in suppliers on i.SupplierID equals s.SupplierID into table1
                         where i.ItemID == id
                         from s in table1.DefaultIfEmpty()
                         select new IndexItemModel
                         {
                             item_details = i,
                             supplier_details = s
                         };

            return View(Record.FirstOrDefault());
        }

        public ActionResult Delete(int id)
        {
            IEnumerable<Item> items = PopulateItems();
            IEnumerable<Supplier> suppliers = PopulateSuppliers();

            var Record = from i in items
                         join s in suppliers on i.SupplierID equals s.SupplierID into table1
                         where i.ItemID == id
                         from s in table1.DefaultIfEmpty()
                         select new IndexItemModel
                         {
                             item_details = i,
                             supplier_details = s
                         };

            return View(Record.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(Item i,int id)
        {
            Item it = new Item();
            it.ItemID = id;

            var deleteTask = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://localhost:44323/API/Items"),
                Content = new StringContent(JsonConvert.SerializeObject(it), Encoding.UTF8, "application/json")
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

        private IEnumerable<Item> PopulateItems()
        {
            IEnumerable<Item> items = null;

            var respondTask = client.GetAsync("Items");
            respondTask.Wait();
            var result = respondTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Item>>();
                readTask.Wait();
                items = readTask.Result;
            }

            return items;
        }

        private IEnumerable<Supplier> PopulateSuppliers()
        {
            IEnumerable<Supplier> suppliers = null;

            var respondTask2 = client.GetAsync("Suppliers");
            respondTask2.Wait();
            var result2 = respondTask2.Result;

            if (result2.IsSuccessStatusCode)
            {
                var readTask2 = result2.Content.ReadAsAsync<IList<Supplier>>();
                readTask2.Wait();
                suppliers = readTask2.Result;
            }

            return suppliers;
        }

        private void PopulateSuppliersDropDownList(Item i = null)
        {
            IEnumerable<Supplier> suppliers = PopulateSuppliers();

            if (i == null)
            {
                ViewBag.SupplierID = new SelectList(suppliers.ToList(), "SupplierID", "SupplierName");
            }
            else
            {
                ViewBag.SupplierID = new SelectList(suppliers.ToList(), "SupplierID", "SupplierName", i.SupplierID);
            }
        }

    }
}