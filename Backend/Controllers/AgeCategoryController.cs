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
    public class AgeCategoryController : Controller
    {

        private readonly Models.AuthenticationContext _database;

        public AgeCategoryController(Models.AuthenticationContext database)
        {
            _database = database;

            //prevent delition of all books from db
            if (_database.AgeCategories.Count() == 0)
            {
                _database.AgeCategories.Add(new AgeCategory { AgeCate = "Default" });
                _database.SaveChanges();
            }
        }


        // GET: AgeCategory  {Get all Categories in table}
        [HttpGet]
        public IEnumerable<AgeCategory> GetallCategories()
        {
            //wait for db response to get Age Category from database
            try
            {
                return _database.AgeCategories.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET AgeCategory/5
        [HttpGet("{id}", Name = "GetAgeCat")]
        public IActionResult Get(int id)
        {
            var cat = _database.AgeCategories.Find(id);
            if (cat == null)
            {
                return NotFound();
            }

            return Ok(cat);
        }

        // POST api/AgeCategory {Add a  new record to the table}
        [HttpPost]
        public IActionResult AddCategory([FromBody]AgeCategory Cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var created = _database.AgeCategories.Add(Cat);
                _database.AgeCategories.Add(Cat);
                _database.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtAction("GetAgeCat", new { id = Cat.AgeCatId }, Cat);

        }


        [HttpPut]
        public IActionResult PutCategory(int id, [FromBody] AgeCategory cate)
        {
            if (id != cate.AgeCatId)
            {
                return BadRequest();
            }
            try
            {
                _database.Entry(cate).State = EntityState.Modified;

                _database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgecatExists(id))
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


        // DELETE api/Cat/5  {Delete Cate at id =}
        [HttpDelete("{id}")]

        public IActionResult DeleteCategory(int id)
        {
            var delcat = _database.AgeCategories.Find(id);

            if (delcat == null)
            {
                return NotFound();
            }

            _database.AgeCategories.Remove(delcat);
            _database.SaveChanges();

            return new JsonResult(new { message = "Category Deleted" });
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
        private bool AgecatExists(int id)
        {
            return _database.AgeCategories.Count(e => e.AgeCatId == id) > 0;
        }
    }

}
