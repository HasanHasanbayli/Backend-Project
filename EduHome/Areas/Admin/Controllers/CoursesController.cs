using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduHome.DAL;
using EduHome.Models;
using EduHome.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using EduHome.Extentions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduHome.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoursesController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _db;
        public CoursesController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Courses.Include(c=>c.CategoryCourses).ThenInclude(x=>x.Category).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _db.Categories.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCoursesVM createCoursesVM, List<int> List)
        {
            ViewBag.Categories = _db.Categories.ToList();
            if (!ModelState.IsValid) return View();
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!createCoursesVM.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                return View();
            }
            if (createCoursesVM.Photo.MaxLength(200))
            {
                ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                return View();
            }
            string path = Path.Combine("img", "course");
            string filelName = await createCoursesVM.Photo.SaveImg(_env.WebRootPath, path);
            Courses newCourses = new Courses
            {
                Image = filelName,
                Title =createCoursesVM.Title,
                Description = createCoursesVM.Description,
                AboutCourses = createCoursesVM.AboutCourses,
                HowToApply = createCoursesVM.HowToApply,
                Certification = createCoursesVM.Certification,
                Starts = createCoursesVM.Starts,
                Duration = createCoursesVM.Duration,
                ClassDuration = createCoursesVM.ClassDuration,
                SkillLevel = createCoursesVM.SkillLevel,
                Language = createCoursesVM.Language,
                Students = createCoursesVM.Students,
                Assesments = createCoursesVM.Assesments,
                CourseFee=createCoursesVM.CourseFee
            };
            List<CategoryCourses> categoryCourses = new List<CategoryCourses>();
            foreach (var item in List)
            {
                CategoryCourses newCategoryCourse = new CategoryCourses
                {
                    CoursesId = newCourses.Id,
                    CategoryId = item
                };
                categoryCourses.Add(newCategoryCourse);
            }
            newCourses.CategoryCourses = categoryCourses;
            await _db.Courses.AddAsync(newCourses);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Courses courses = await _db.Courses.FindAsync(id);
            if (courses == null) return NotFound();
            return View(courses);
        }
        public IActionResult Update(int? id)
        {
            ViewBag.Categories = _db.Categories.ToList();
            if (id == null) return NotFound();
            Courses courses = _db.Courses.Include(d => d.CategoryCourses).ThenInclude(x => x.Courses).FirstOrDefault(p => p.Id == id);
            if (courses == null) return NotFound();
            return View(courses);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Courses courses, List<int> List)
        {
            ViewBag.Categories = _db.Categories.ToList();
            if (id == null) return NotFound();
            Courses dbCourses = _db.Courses.Include(d=>d.CategoryCourses).ThenInclude(x=>x.Courses).FirstOrDefault(c=>c.Id==id);
            if (dbCourses == null) return NotFound();
            if (courses.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!courses.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }
                if (courses.Photo.MaxLength(2000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 200kb ola biler");
                    return View();
                }
                string path = Path.Combine("img", "course");
                Helper.DeleteImage(_env.WebRootPath, path, dbCourses.Image);
                string fileName = await courses.Photo.SaveImg(_env.WebRootPath, path);
                dbCourses.Image = fileName;
            }
            dbCourses.Title = courses.Title;
            dbCourses.Description = courses.Description;
            dbCourses.AboutCourses = courses.Description;
            dbCourses.HowToApply = courses.HowToApply;
            dbCourses.Certification = courses.Certification;
            dbCourses.Starts = courses.Starts;
            dbCourses.Duration = courses.Duration;
            dbCourses.ClassDuration = courses.ClassDuration;
            dbCourses.SkillLevel = courses.SkillLevel;
            dbCourses.Language = courses.Language;
            dbCourses.Students = courses.Students;
            dbCourses.Assesments = courses.Assesments;
            dbCourses.CourseFee = courses.CourseFee;
            List<CategoryCourses> categoryCourses = new List<CategoryCourses>();
            foreach (var item in List)
            {
                CategoryCourses newCategoryCourses = new CategoryCourses
                {
                    CoursesId = dbCourses.Id,
                    CategoryId = item
                };
                categoryCourses.Add(newCategoryCourses);
            }
            dbCourses.CategoryCourses = categoryCourses;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Courses courses = await _db.Courses.FindAsync(id);
            if (courses == null) return NotFound();
            return View(courses);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Courses courses)
        {
            string path = Path.Combine("img", "course");
            if (id == null) return NotFound();
            Courses dbCourses = await _db.Courses.FindAsync(id);
            Helper.DeleteImage(_env.WebRootPath, path, dbCourses.Image);
            if (dbCourses == null) return NotFound();
            _db.Courses.Remove(dbCourses);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
