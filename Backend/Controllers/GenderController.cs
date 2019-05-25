using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OurApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OurApi.Controllers
{
    [Route("api/[controller]")]
    public class GenderController : Controller
    {
        private readonly Models.AuthenticationContext _database;

        public GenderController(Models.AuthenticationContext database)
        {
            _database = database;

            //prevent delition of all books from db
            if (_database.Genders.Count() == 0)
            {
                _database.Genders.Add(new Gender { GenType = "Default" });
                _database.SaveChanges();
            }
        }

        // GET: books  {Get all books in table}
        [HttpGet]
        public IEnumerable<Gender> GetallGenders()
        {
            //wait for db response to get Books from database
            try
            {
                return _database.Genders.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET gender/5
        [HttpGet("{id}", Name = "GetGender")]
        public IActionResult Get(int id)
        {
            var gender = _database.Genders.Find(id);
            if (gender == null)
            {
                return NotFound();
            }

            return Ok(gender);
        }

        // POST api/gender  {Add a  new record to the table}
        [HttpPost]
        public IActionResult AddGender([FromBody]Gender gender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var created = _database.Genders.Add(gender);
                _database.Genders.Add(gender);
                _database.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtAction("GetGender", new { id =  gender.GenId }, gender);

        }


        [HttpPut]
        public IActionResult PutGender(int id, [FromBody] Gender gender)
        {
            if (id != gender.GenId)
            {
                return BadRequest();
            }
            try
            {
                _database.Entry(gender).State = EntityState.Modified;

                _database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
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


        // DELETE api/  Gender/5  {Delete Gender at id =}
        [HttpDelete("{id}")]

        public IActionResult DeleteGender(int id)
        {
            var delGen = _database.Genders.Find(id);

            if (delGen == null)
            {
                return NotFound();
            }

            _database.Genders.Remove(delGen);
            _database.SaveChanges();

            return new JsonResult(new { message = "Gender Deleted" });
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
        private bool GenderExists(int id)
        {
            return _database.Genders.Count(e => e.GenId == id) > 0;
        }
    }
}
