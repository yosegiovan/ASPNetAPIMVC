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
    public class SuppliersController : ApiController
    {
        SupplierRepository sr = new SupplierRepository();

        public IHttpActionResult Post(Supplier s)
        {
            sr.Create(s);
            try
            {
                return Ok();
            }
            catch 
            {
                return Content(HttpStatusCode.BadRequest, "Failed to Create Supplier");
            }
        }

        public IHttpActionResult Get()
        {
            IEnumerable<Supplier> sp = sr.Get();

            try
            {
                //return Ok();
                return Ok(sp);
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, "Failed to Get Supplier");
            }

        }

        public async Task<IHttpActionResult> Get(int SupplierID)
        {
            var test = await sr.Get(SupplierID);

            try
            {
                //return Ok();
                return Ok(test);
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, "Failed to Get Supplier By ID");
            }
        }

        public IHttpActionResult Delete(Supplier s)
        {
            sr.Delete(s.SupplierID);
            try
            {
                return Ok();
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, "Failed to Delete Supplier");
            }
        }

        public IHttpActionResult Put(Supplier s)
        {
            sr.Update(s.SupplierID, s.SupplierName);
            try
            {
                return Ok();
            }
            catch
            {
                return Content(HttpStatusCode.BadRequest, "Failed to Update Supplier");
            }
        }
    }
}
