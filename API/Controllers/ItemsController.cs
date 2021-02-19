using API.Models;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class ItemsController : ApiController
    {
        ItemRepository ir = new ItemRepository();

        public IHttpActionResult Post(Item i)
        {
            ir.Create(i);
            try
            {
                return Ok();
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, "Failed to Create Item");
            }
        }

        public IHttpActionResult Get()
        {
            IEnumerable<Item> it = ir.Get();

            try
            {
                return Ok(it);
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, "Failed to Get Item");
            }
        }

        public async Task<IHttpActionResult> Get(int ItemID)
        {
            var test = await ir.Get(ItemID);

            try
            {
                return Ok(test);
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, "Failed to Get Item By ID");
            }
        }

        public IHttpActionResult Delete(Item i)
        {
            ir.Delete(i.ItemID);
            try
            {
                return Ok();
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, "Failed to Delete Item");
            }
        }

        public IHttpActionResult Put(Item i)
        {
            ir.Update(i.ItemID, i.ItemName, i.Quantity, i.Price, i.SupplierID);

            try
            {
                return Ok();
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, "Failed to Update Item");
            }
        }
    }
}
