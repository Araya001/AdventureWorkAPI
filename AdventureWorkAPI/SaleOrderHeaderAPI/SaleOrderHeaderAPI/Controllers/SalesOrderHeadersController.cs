using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SaleOrderHeaderAPI;
using System.Web.Http.Cors;

namespace SaleOrderHeaderAPI.Controllers
{
    
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SalesOrderHeadersController : ApiController
    {
        private AdventureWorksEntities db = new AdventureWorksEntities();

        /// <summary>
        /// Search SalesOrderHeaders by status
        /// </summary>
        /// <param name="status">Status</param>
        /// <return>Full status record</return>
        [HttpGet]
        [Route("api/v1/salesOrderHeader/Status/{status}")]
        [ResponseType(typeof(SalesOrderHeader))]
        public IHttpActionResult SearchSalesOrderHeaderByStatus(int status)
        {
            IQueryable SalesOrderHeader = db.SalesOrderHeaders.Where(x => x.Status == status);
            if (SalesOrderHeader == null)
            {
                return NotFound();
            }

            return Ok(SalesOrderHeader);

        }


        /// <summary>
        /// Search SalesOrderHeaders by customer id
        /// </summary>
        /// <param name="CusId">Customer ID</param>
        /// <returns>Full salesOrderHeader</returns>
        [HttpGet]
        [Route("api/v1/salesOrderHeader/CustomerId/{CusId}")]
        [ResponseType(typeof(SalesOrderHeader))]
        public IHttpActionResult SearchSalesOrderHeaderByCustomerID(int CusId)
        {
            IQueryable salesOrderHeader = db.SalesOrderHeaders.Where(x => x.CustomerID==CusId);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }

            return Ok(salesOrderHeader);
        }


        /// <summary>
        /// Search SalesOrderHeaders by sale order id
        /// </summary>
        /// <param name="SaleOrderId">SaleOrder ID</param>
        /// <returns>Full salesOrderHeader</returns>
        [HttpGet]
        [Route("api/v1/salesOrderHeader/SalesOrderID/{SaleOrderId}")]
        [ResponseType(typeof(SalesOrderHeader))]
        public IHttpActionResult GetSalesOrderHeader(int SaleOrderId)
        {
            IQueryable salesOrderHeader = db.SalesOrderHeaders.Where(x => x.SalesOrderID == SaleOrderId);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }

            return Ok(salesOrderHeader);
        }


        /// <summary>
        /// Edit SalesOrderHeaders
        /// </summary>
        /// <param name="SaleOrderId">SaleOrder ID</param>
        /// <returns>Full salesOrderHeader</returns>
        [Route("api/v1/salesOrderHeader/UpdateSalesOrderID/{SaleOrderId}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalesOrderHeader(int SaleOrderId, SalesOrderHeader salesOrderHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (SaleOrderId != salesOrderHeader.SalesOrderID)
            {
                return BadRequest();
            }

            db.Entry(salesOrderHeader).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesOrderHeaderExists(SaleOrderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adding SalesOrderHeaders
        /// </summary>
        /// <returns>Full salesOrderHeader</returns>
        [Route("api/v1/salesOrderHeader/AddSalesOrderID/{SaleOrderId}")]
        [ResponseType(typeof(SalesOrderHeader))]
        public IHttpActionResult PostSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesOrderHeaders.Add(salesOrderHeader);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SalesOrderHeaderExists(salesOrderHeader.SalesOrderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salesOrderHeader.SalesOrderID }, salesOrderHeader);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesOrderHeaderExists(int id)
        {
            return db.SalesOrderHeaders.Count(e => e.SalesOrderID == id) > 0;
        }
    }
}