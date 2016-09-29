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
using OnlineBusTicketReservationSystem.Core;

namespace OnlineBusTicketReservationSystem.WebAPI.Areas.Admin.Controllers
{
    /// <summary>
    ///Admin operations for Bus Details Modifications and add,edit or 
    /// delete Bus details with web API.
    /// Edit By Shruti Kulkarni
    /// Date 5/9/2016
    /// </summary>
    public class AdminBusDetailsController : ApiController
    {
        private OnlineBusTicketReservationSystemEntities dataEntity = new OnlineBusTicketReservationSystemEntities();

        // GET api/AdminBusDetails
        public IQueryable<tbl_BusDetails> Gettbl_BusDetails()
        {
            return dataEntity.tbl_BusDetails;
        }

        // GET api/AdminBusDetails/5
        [ResponseType(typeof(tbl_BusDetails))]
        public IHttpActionResult Gettbl_BusDetails(int id)
        {
            tbl_BusDetails tbl_busdetails = dataEntity.tbl_BusDetails.Find(id);
            if (tbl_busdetails == null)
            {
                return NotFound();
            }

            return Ok(tbl_busdetails);
        }

        // PUT api/AdminBusDetails/5
        public IHttpActionResult Puttbl_BusDetails(int id, tbl_BusDetails tbl_busdetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_busdetails.BusNumber)
            {
                return BadRequest();
            }

            dataEntity.Entry(tbl_busdetails).State = EntityState.Modified;

            try
            {
                dataEntity.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_BusDetailsExists(id))
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

        // POST api/AdminBusDetails
        [ResponseType(typeof(tbl_BusDetails))]
        public IHttpActionResult Posttbl_BusDetails(tbl_BusDetails tbl_busdetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dataEntity.tbl_BusDetails.Add(tbl_busdetails);
            dataEntity.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_busdetails.BusNumber }, tbl_busdetails);
        }

        // DELETE api/AdminBusDetails/5
        [ResponseType(typeof(tbl_BusDetails))]
        public IHttpActionResult Deletetbl_BusDetails(int id)
        {
            tbl_BusDetails tbl_busdetails = dataEntity.tbl_BusDetails.Find(id);
            if (tbl_busdetails == null)
            {
                return NotFound();
            }

            dataEntity.tbl_BusDetails.Remove(tbl_busdetails);
            dataEntity.SaveChanges();

            return Ok(tbl_busdetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dataEntity.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_BusDetailsExists(int id)
        {
            return dataEntity.tbl_BusDetails.Count(e => e.BusNumber == id) > 0;
        }
    }
}