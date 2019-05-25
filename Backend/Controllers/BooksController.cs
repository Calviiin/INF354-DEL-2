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
    public class BooksController : Controller
    {
        private readonly Models.AuthenticationContext _database;

        public BooksController(Models.AuthenticationContext database)
        {
            _database = database;

            //prevent delition of all books from db
            if (_database.Books.Count() ==0 )
            {
                _database.Books.Add(new Books { Name = "Default" });
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
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult Get(int id)
        {
            var book = _database.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST api/books  {Add a  new record to the table}
        [HttpPost]
        public IActionResult AddBook([FromBody]Books Nbook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var created = _database.Books.Add(Nbook);
                _database.Books.Add(Nbook);
                _database.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return CreatedAtAction("GetBook", new { id = Nbook.Id }, Nbook);

        }


        [HttpPut]
        public IActionResult PutBook(int id,[FromBody] Books book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            try
            {
                _database.Entry(book).State = EntityState.Modified;

                _database.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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


        // DELETE api/Book/5  {Delete book at id =}
        [HttpDelete("{id}")]
        
        public  IActionResult DeleteBook(int id)
        {
            var delBook = _database.Books.Find(id);

            if(delBook == null)
            {
                return NotFound();
            }

            _database.Books.Remove(delBook);
            _database.SaveChanges();

            return new JsonResult(new { message = "Book Deleted" });
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
        private bool BookExists(int id)
        {
            return _database.Books.Count(e => e.Id == id) > 0;
        }


    }
}
