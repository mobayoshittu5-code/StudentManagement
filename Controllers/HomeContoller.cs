using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using StudentManagement.Data;
//using System.Linq;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Students()
        {
            var students = await _context.Students.ToListAsync();

            return View(students);
        }
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();

            int totalStudents = students.Count;

            int passingStudents = students.Count(s => s.Score >= 50);

            int failingStudents = students.Count(s => s.Score < 50);

            int totalCourses = students
                .Select(s => s.Course)
                 .Where(c => !string.IsNullOrWhiteSpace(c))
                 .Distinct()
                  .Count();

            ViewBag.TotalStudents = totalStudents;
            ViewBag.PassingStudents = passingStudents;
            ViewBag.FailingStudents = failingStudents;
            ViewBag.TotalCourses = totalCourses;

            return View();
        }
        public IActionResult About()
        {
            ViewData["Name"] = "Mobayo Shittu Akinwale";
            ViewData["Age"] = 25;
            ViewData["Course"] = "Computer Science";
            return View();
        }
        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Students");
        }
        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> EditStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(student);
            }

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Students");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Students");
        }
    }
}
