using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduHome.DAL;
using EduHome.Extentions;
using EduHome.Helpers;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _db;
        public TeacherController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Teachers.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (!ModelState.IsValid) return View();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!teacher.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }
            if (teacher.Photo.MaxLength(2000))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                return View();
            }
            string path = Path.Combine("img", "teacher");
            string fileName = await teacher.Photo.SaveImg(_env.WebRootPath, path);
            Teacher newTeacher = new Teacher();
            newTeacher.FullName = teacher.FullName;
            newTeacher.Image = await teacher.Photo.SaveImg(_env.WebRootPath, "img/teacher");
            newTeacher.Position = teacher.Position;
            newTeacher.About = teacher.About;
            newTeacher.Degree = teacher.Degree;
            newTeacher.Experience = teacher.Experience;
            newTeacher.Hobbies = teacher.Hobbies;
            newTeacher.Faculty = teacher.Faculty;
            newTeacher.Mail = teacher.Mail;
            newTeacher.Call = teacher.Call;
            newTeacher.Skype = teacher.Skype;
            newTeacher.Facebook = teacher.Facebook;
            newTeacher.Pinterest = teacher.Pinterest;
            newTeacher.Vkontakte = teacher.Vkontakte;
            newTeacher.Twitter = teacher.Twitter;
            await _db.Teachers.AddAsync(newTeacher);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _db.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = _db.Teachers.FirstOrDefault(p => p.Id == id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Teacher teacher)
        {
            if (id == null) return NotFound();
            Teacher dbTeacher = await _db.Teachers.FindAsync(id);
            if (dbTeacher == null) return NotFound();
            if (teacher.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!teacher.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }
                if (teacher.Photo.MaxLength(2000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                    return View();
                }
                string path = Path.Combine("img", "teacher");
                Helper.DeleteImage(_env.WebRootPath, path, dbTeacher.Image);
                string fileName = await teacher.Photo.SaveImg(_env.WebRootPath, path);
                dbTeacher.Image = fileName;
            }
            dbTeacher.FullName = teacher.FullName;
            dbTeacher.Position = teacher.Position;
            dbTeacher.About = teacher.About;
            dbTeacher.Degree = teacher.Degree;
            dbTeacher.Experience = teacher.Experience;
            dbTeacher.Faculty = teacher.Faculty;
            dbTeacher.Mail = teacher.Mail;
            dbTeacher.Call = teacher.Call;
            dbTeacher.Skype = teacher.Skype;
            dbTeacher.Facebook = teacher.Facebook;
            dbTeacher.Pinterest = teacher.Pinterest;
            dbTeacher.Vkontakte = teacher.Vkontakte;
            dbTeacher.Twitter = teacher.Twitter;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Teacher teacher = await _db.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Teacher teacher)
        {
            if (id == null) return NotFound();
            Teacher dbTeacher = await _db.Teachers.FindAsync(id);
            if (dbTeacher == null) return NotFound();
            _db.Teachers.Remove(dbTeacher);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
