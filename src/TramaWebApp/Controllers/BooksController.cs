using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using TramaWebApp.Models;
using Windows.Foundation;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.UI;
using Microsoft.AspNet.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Authorization;

namespace TramaWebApp.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;
        IApplicationEnvironment _hostingEnvironment;

        public BooksController(ApplicationDbContext context, IApplicationEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Books
        [AllowAnonymous]
        public IActionResult Index(string filter)
        {                          
            return View(_context.books
                .Where(x => filter == null || (x.Title.Contains(filter)) || x.Author.Contains(filter)));
                          
        }

        // GET: Books/Details/5
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            
            Book book = _context.books.Include(b => b.Essays).Single(m => m.BookId == id);
            

                if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var book = new Book();
            
            return View(book);
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Book myBook = book;
                myBook.Essays = new List<Essay>();
                

                // Add a picture of the book
                var fileName = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');

                if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png"))
                {
                    var filePath = _hostingEnvironment.ApplicationBasePath + "\\wwwroot\\" + fileName;
                    await file.SaveAsAsync(filePath);
                    myBook.Image = fileName;
                }

                _context.books.Add(myBook);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Book book = _context.books.Single(m => m.BookId == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Book myBook = book;

                // Add a picture of the book
                var fileName = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');

                if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png"))
                {
                    var filePath = _hostingEnvironment.ApplicationBasePath + "\\wwwroot\\" + fileName;
                    await file.SaveAsAsync(filePath);
                    myBook.Image = fileName;
                }
                _context.Update(myBook);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Book book = _context.books.Single(m => m.BookId == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Book book = _context.books.Single(m => m.BookId == id);
            _context.books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


     

      
    }
}
