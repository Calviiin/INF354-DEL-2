
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
    public class RegionsController : Controller
    {
        private readonly Models.AuthenticationContext _database;

        public RegionsController(Models.AuthenticationContext database)
        {
            _database = database;

            //prevent delition of all books from db
            if (_database.Regions.Count() == 0)
            {
                _database.Regions.Add(new Regions { RegionName = "Default" });
                _database.SaveChanges();
            }
        }

        // GET: books  {Get all books in table}
        [HttpGet]
        public IEnumerable<Regions> GetallRegions()
        {
            //wait for db response to get Books from database
            try
            {
                return _database.Regions.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET Region/5
        [HttpGet("{id}", Name = "GetRegion")]
        public IActionResult Get(int id)
        {
            var region = _database.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

        // POST api/regions  {Add a  new record to the table}
        [HttpPost]
        public IActionResult AddBook([FromBody]Regions region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var created = _database.Regions.Add(region);
                _database.Regions.Add(region);
                _database.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtAction("GetRegion", new { id = region.RegionId }, region);

        }


        [HttpPut]
        public IActionResult PutBook(int id, [FromBody]  Regions region)
        {
            if (id != region.RegionId)
            {
                return BadRequest();
            }
            try
            {
                _database.Entry(region).State = EntityState.Modified;

                _database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(id))
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


        // DELETE api/Region/5  {Delete Region at id =}
        [HttpDelete("{id}")]

        public IActionResult DeleteRegion(int id)
        {
            var delRegion = _database.Regions.Find(id);

            if (delRegion == null)
            {
                return NotFound();
            }

            _database.Regions.Remove(delRegion);
            _database.SaveChanges();

            return new JsonResult(new { message = "Region Deleted" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _database.Dispose();
            }
            base.Dispose(disposing);
        }

        // checking for  api/Region/5  {locate Region at id =}
        private bool RegionExists(int id)
        {
            return _database.Regions.Count(e => e.RegionId == id) > 0;
        }
    }
   }

