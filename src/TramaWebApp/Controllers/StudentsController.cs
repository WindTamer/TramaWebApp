using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using TramaWebApp.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;

namespace TramaWebApp.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Students
        [AllowAnonymous]
        public IActionResult Index(string filter)
        {
            return View(_context.students
                .Where(x => filter == null || (x.StudentName.Contains(filter))));
        }

        // GET: Students/Details/5
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Student student = _context.students.Include(s => s.Essays).Single(m => m.StudentId == id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                Student myStudent = student;
                
                myStudent.Essays = new List<Essay>();

                _context.students.Add(myStudent);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Student student = _context.students.Single(m => m.StudentId == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Student student = _context.students.Single(m => m.StudentId == id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Student student = _context.students.Single(m => m.StudentId == id);
            _context.students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
