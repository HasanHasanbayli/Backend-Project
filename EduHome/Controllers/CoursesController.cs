using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _db;
        public CoursesController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Courses.ToList());
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Courses courses = _db.Courses.FirstOrDefault(c=>c.Id==id);
            if (courses == null) return NotFound();
            return View(courses);
        }
    }
}
