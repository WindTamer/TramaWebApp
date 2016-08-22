using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using TramaWebApp.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.Http.Internal;
using System.Collections.Immutable;
using Microsoft.AspNet.Authorization;

namespace TramaWebApp.Controllers
{
    [Authorize]
    public class EssaysController : Controller
    {
        private ApplicationDbContext _context;


        public EssaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Essays
        [AllowAnonymous]
        public IActionResult Index(string filter)
        {
            return View(_context.essays.Include(e => e.book).Include(e => e.student)
                .Where(x => filter == null || (x.EssayTitle.Contains(filter)) || x.StudentName.Contains(filter) || x.book.Author.Contains(filter) || x.BookTitle.Contains(filter)));
        }

        // GET: Essays/Details/5
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Essay essay = _context.essays.Include(b => b.book).Include(s => s.student).Single(m => m.EssayId == id);
            if (essay == null)
            {
                return HttpNotFound();
            }

            return View(essay);
        }

        // GET: Essays/Create
        public IActionResult Create()
        { 

            var students = _context.students.OrderBy(st => st.StudentName).ToList();
            SelectList s = new SelectList(students, "StudentId", "StudentName");
            ViewData["SelectedStudent"] = s;


            var books = _context.books.OrderBy(b => b.Title).ToList();
            SelectList bk = new SelectList(books, "BookId", "Title");
            ViewData["SelectedBook"] = bk;



            return View();
        }

        // POST: Essays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Essay essay, int? SelectedBook, int? SelectedStudent)
        {
            Essay myEssay = essay;

            if (ModelState.IsValid)
            {
                // Add selected student to the essay object and to the StudentBook object
                var students = _context.students.OrderBy(s => s.StudentName).ToList();
                ViewBag.SelectedStudent = new SelectList(students, "StudentID", "StudentName", SelectedStudent);
                int studId = SelectedStudent.GetValueOrDefault();
                foreach (Student st in _context.students)
                {
                    if (st.StudentId == studId)
                    {
                        myEssay.student = st;
                        myEssay.StudentName = st.StudentName;
                        
                    }
             
                }

                // Add selected book to the essay object
                var books = _context.books.OrderBy(s => s.Title).ToList();
                ViewBag.SelectedBook = new SelectList(books, "BookId", "Title", SelectedBook);
                int bkId = SelectedBook.GetValueOrDefault();
                foreach (Book bk in _context.books)
                {
                    if (bk.BookId == bkId)
                    {
                        myEssay.book = bk;
                        myEssay.BookTitle = bk.Title;
                        
                    }
                }


                _context.essays.Add(myEssay);
                _context.SaveChanges();


                return RedirectToAction("Index");
            }
            return View(myEssay);
        }

        // GET: Essays/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Essay essay = _context.essays.Include(b => b.book).Include(s => s.student).Single(m => m.EssayId == id);

            var students = _context.students.OrderBy(p => p.StudentName).ToList();
            SelectList stud = new SelectList(students, "StudentId", "StudentName");
            ViewData["SelectedStudent"] = stud;


            var books = _context.books.OrderBy(b => b.Title).ToList();
            SelectList bk = new SelectList(books, "BookId", "Title");
            ViewData["SelectedBook"] = bk;

            if (essay == null)
            {
                return HttpNotFound();
            }
            return View(essay);
        }

        // POST: Essays/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Essay essay, int? SelectedBook, int? SelectedStudent)
        {
            Essay myEssay = essay;

            if (ModelState.IsValid)
            {
                // Add selected student to the essay object and to the StudentBook object
                var students = _context.students.OrderBy(s => s.StudentName).ToList();
                ViewBag.SelectedStudent = new SelectList(students, "StudentID", "StudentName", SelectedStudent);
                int studId = SelectedStudent.GetValueOrDefault();
                foreach (Student st in _context.students)
                {
                    if (st.StudentId == studId)
                    {
                        myEssay.student = st;

                    }

                }

                // Add selected book to the essay object
                var books = _context.books.OrderBy(s => s.Title).ToList();
                ViewBag.SelectedBook = new SelectList(books, "BookId", "Title", SelectedBook);
                int bkId = SelectedBook.GetValueOrDefault();
                foreach (Book bk in _context.books)
                {
                    if (bk.BookId == bkId)
                    {
                        myEssay.book = bk;

                    }
                }

                

                _context.Update(myEssay);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myEssay);
        }

        // GET: Essays/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Essay essay = _context.essays.Single(m => m.EssayId == id);
            if (essay == null)
            {
                return HttpNotFound();
            }

            return View(essay);
        }

        // POST: Essays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {


            Essay essay = _context.essays.Single(m => m.EssayId == id);
            _context.essays.Remove(essay);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
