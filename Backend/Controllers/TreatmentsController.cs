
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OurApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace OurApi.Controllers
{
    [Route("api/[controller]")]
    public class TreatmentsController : Controller
    {
        private readonly Models.AuthenticationContext _database;

        public TreatmentsController(Models.AuthenticationContext database)
        {
            _database = database;

            //prevent delition of all books from db
            if (_database.Treatments.Count() == 0)
            {
                _database.Treatments.Add(new Treatments { TreatName = "Default" });
                _database.SaveChanges();
            }
        }
        // GET: Treatments  {Get all Treatments in table}
        [HttpGet]
        public IEnumerable<Treatments> GetallTreatments()
        {
            //wait for db response to get Treatments from database
            try
            {
                return _database.Treatments.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET Tratment/5
        [HttpGet("{id}", Name = "GetTreatment")]
        public IActionResult Get(int id)
        {
            var treat = _database.Treatments.Find(id);
            if (treat == null)
            {
                return NotFound();
            }

            return Ok(treat);
        }

        // POST api/Treatments  {Add a  new record to the table}
        [HttpPost]
        public IActionResult AddTreatment([FromBody]Treatments treat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var created = _database.Treatments.Add(treat);
                _database.Treatments.Add(treat);
                _database.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtAction("GetTreatment", new { id = treat.TreatId }, treat);

        }


        [HttpPut]
        public IActionResult PutTreat(int id, [FromBody] Treatments treat)
        {
            if (id != treat.TreatId)
            {
                return BadRequest();
            }
            try
            {
                _database.Entry(treat).State = EntityState.Modified;

                _database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(new { message = "Updated" });
        }

        private IActionResult StatusCode(HttpStatusCode noContent)
        {
            throw new NotImplementedException();
        }


        // DELETE api/Treat/5  {Delete Treat at id =}
        [HttpDelete("{id}")]

        public IActionResult DeleteTreat(int id)
        {
            var deltreat = _database.Treatments.Find(id);

            if (deltreat == null)
            {
                return NotFound();
            }

            _database.Treatments.Remove(deltreat);
            _database.SaveChanges();

            return new JsonResult(new { message = "Treatment Deleted" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _database.Dispose();
            }
            base.Dispose(disposing);
        }

        // checking for  api/Book/5  {locate book at id =}
        private bool TreatExists(int id)
        {
            return _database.Treatments.Count(e => e.TreatId == id) > 0;
        }
    }
   
}
