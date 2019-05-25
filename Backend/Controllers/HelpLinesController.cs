
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
    public class HelpLinesController : Controller
    {
        private readonly Models.AuthenticationContext _database;

        public HelpLinesController(Models.AuthenticationContext database)
        {
            _database = database;

            //prevent delition of all Numbers from db
            if (_database.HelpLines.Count() == 0)
            {
                _database.HelpLines.Add(new HelpLines { HelpName  = "Default" });
                _database.SaveChanges();
            }
        }
        // GET: Help  {Get all Help in table}
        [HttpGet]
        public IEnumerable<HelpLines> GetallHelp()
        {
            //wait for db response to get Numbers from database
            try
            {
                return _database.HelpLines.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET help/5
        [HttpGet("{id}", Name = "GetHelp")]
        public IActionResult Get(int id)
        {
            var help = _database.HelpLines.Find(id);
            if (help == null)
            {
                return NotFound();
            }

            return Ok(help);
        }

        // POST api/Help  {Add a  new record to the table}
        [HttpPost]
        public IActionResult AddHelp([FromBody] HelpLines help)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var created = _database.HelpLines.Add(help);
                _database.HelpLines.Add(help);
                _database.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtAction("GetHelp", new { id = help.HelpId }, help);

        }


        [HttpPut]
        public IActionResult PutHelp(int id, [FromBody] HelpLines help)
        {
            if (id != help.HelpId)
            {
                return BadRequest();
            }
            try
            {
                _database.Entry(help).State = EntityState.Modified;

                _database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HelpExists(id))
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


        // DELETE api/Help/5  {Delete HelpLine at id =}
        [HttpDelete("{id}")]

        public IActionResult DeleteHelp(int id)
        {
            var delHelp = _database.HelpLines.Find(id);

            if (delHelp == null)
            {
                return NotFound();
            }

            _database.HelpLines.Remove(delHelp);
            _database.SaveChanges();

            return new JsonResult(new { message = "Help Line  Deleted" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _database.Dispose();
            }
            base.Dispose(disposing);
        }

        // checking for  api/Help/5  {locate Help at id =}
        private bool HelpExists(int id)
        {
            return _database.HelpLines.Count(e => e.HelpId == id) > 0;
        }
    }
}
