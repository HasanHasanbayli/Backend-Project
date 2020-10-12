using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _db;
        public TeacherController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Teachers.ToList().Take(8));
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = _db.Teachers.Include(s => s.Skill).FirstOrDefault(t=>t.Id==id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
    }
}
