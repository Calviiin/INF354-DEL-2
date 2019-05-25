
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
    public class SmokerStatusController : Controller
    {
        private readonly Models.AuthenticationContext _database;

        public SmokerStatusController(Models.AuthenticationContext database)
        {
            _database = database;

        }

        // GET: books  {Get all books in table}
        [HttpGet]
        public IEnumerable<SmokerStat> GetallStat()
        {
            //wait for db response to get Books from database
            try
            {
                return _database.SmokerStats.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET stat/5
        [HttpGet("{id}", Name = "GetStat")]
        public IActionResult Get(int id)
        {
            var stat = _database.SmokerStats.Find(id);
            if (stat == null)
            {
                return NotFound();
            }

            return Ok(stat);
        }

        // POST api/Smoker  {Add a  new record to the table}
        [HttpPost]
        public IActionResult AddStat([FromBody] SmokerStat stat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var created = _database.SmokerStats.Add(stat);
                _database.SmokerStats.Add(stat);
                _database.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtAction("GetStat", new { id = stat.SmokId }, stat);

        }


        [HttpPut]
        public IActionResult PutStat(int id, [FromBody] SmokerStat stat)
        {
            if (id !=stat.SmokId)
            {
                return BadRequest();
            }
            try
            {
                _database.Entry(stat).State = EntityState.Modified;

                _database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmokerExists(id))
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


        // DELETE api/Smoker/5  {Delete SmokerStat at id =}
        [HttpDelete("{id}")]

        public IActionResult DeleteStat(int id)
        {
            var delsats = _database.SmokerStats.Find(id);

            if (delsats == null)
            {
                return NotFound();
            }

            _database.SmokerStats.Remove(delsats);
            _database.SaveChanges();

            return new JsonResult(new { message = "Smoker Stats Deleted" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _database.Dispose();
            }
            base.Dispose(disposing);
        }

        // checking for  api/Smoker/5  {locate book at id =}
        private bool SmokerExists(int id)
        {
            return _database.SmokerStats.Count(e => e.SmokId == id) > 0;
        }
    }

}
