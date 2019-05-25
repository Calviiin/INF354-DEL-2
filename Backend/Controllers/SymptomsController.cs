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
    public class SymptomsController : Controller
    {
        private readonly Models.AuthenticationContext _database;

        public SymptomsController(Models.AuthenticationContext database)
        {
            _database = database;

            //prevent delition of all books from db
            if (_database.Symptoms.Count() == 0)
            {
                _database.Symptoms.Add(new Symptoms { SympName = "Default" });
                _database.SaveChanges();
            }
        }

        // GET: books  {Get all books in table}
        [HttpGet]
        public IEnumerable<Symptoms> GetallSympt()
        {
            //wait for db response to get Books from database
            try
            {
                return _database.Symptoms.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET symp/5
        [HttpGet("{id}", Name = "GetSymp")]
        public IActionResult Get(int id)
        {
            var symp = _database.Symptoms.Find(id);
            if (symp == null)
            {
                return NotFound();
            }

            return Ok(symp);
        }

        // POST api/Symp  {Add a  new record to the table}
        [HttpPost]
        public IActionResult AddSymp([FromBody]Symptoms symp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var created = _database.Symptoms.Add(symp);
                _database.Symptoms.Add(symp);
                _database.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtAction("GetSymp", new { id = symp.SympId }, symp);

        }


        [HttpPut]
        public IActionResult PutBook(int id, [FromBody] Symptoms symp)
        {
            if (id !=symp.SympId)
            {
                return BadRequest();
            }
            try
            {
                _database.Entry(symp).State = EntityState.Modified;

                _database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SympExists(id))
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


        // DELETE api/Symp/5  {Delete Symp at id =}
        [HttpDelete("{id}")]

        public IActionResult DeleteSymp(int id)
        {
            var delsymp = _database.Symptoms.Find(id);

            if (delsymp == null)
            {
                return NotFound();
            }

            _database.Symptoms.Remove(delsymp);
            _database.SaveChanges();

            return new JsonResult(new { message = "Symptom Deleted" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _database.Dispose();
            }
            base.Dispose(disposing);
        }

        // checking for  api/Symp/5  {locate Symptom at id =}
        private bool SympExists(int id)
        {
            return _database.Symptoms.Count(e => e.SympId == id) > 0;
        }
    }

}
