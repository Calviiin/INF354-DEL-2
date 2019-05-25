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
    public class AboutController : Controller
    {

        private readonly Models.AuthenticationContext _database;

        public AboutController(Models.AuthenticationContext database)
        {
            _database = database;

            //prevent delition of all books from db
            if (_database.Abouts.Count() == 0)
            {
                _database.Abouts.Add(new About { AboutName = "Default" });
                _database.SaveChanges();
            }
        }

        // GET: books  {Get all books in table}
        [HttpGet]
        public IEnumerable<Books> Getallbooks()
        {
            //wait for db response to get Books from database
            try
            {
                return _database.Books.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET book/5
        [HttpGet("{id}", Name = "GetAbout")]
        public IActionResult Get(int id)
        {
            var about = _database.Abouts.Find(id);
            if (about == null)
            {
                return NotFound();
            }

            return Ok(about);
        }

        // POST api/abouts  {Add a  new record to the table}
        [HttpPost]
        public IActionResult AddAbout([FromBody] About about
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var created = _database.Abouts.Add(about);
                _database.Abouts.Add(about);
                _database.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtAction("GetAbout", new { id = about.AboutId }, about);

        }


        [HttpPut]
        public IActionResult PutAbout(int id, [FromBody] About about)
        {
            if (id != about.AboutId)
            {
                return BadRequest();
            }
            try
            {
                _database.Entry(about).State = EntityState.Modified;

                _database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AboutExists(id))
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


        // DELETE api/About/5  {Delete About at id =}
        [HttpDelete("{id}")]

        public IActionResult DeleteAbout(int id)
        {
            var delabout = _database.Abouts.Find(id);

            if (delabout == null)
            {
                return NotFound();
            }

            _database.Abouts.Remove(delabout);
            _database.SaveChanges();

            return new JsonResult(new { message = "Text Field Deleted" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _database.Dispose();
            }
            base.Dispose(disposing);
        }

        // checking for  api/About/5  {locate About at id =}
        private bool AboutExists(int id)
        {
            return _database.Abouts.Count(e => e.AboutId == id) > 0;
        }
    }
}

