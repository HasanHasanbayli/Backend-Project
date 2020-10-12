using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.DAL;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Categories.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryVM createCategoryVM)
        {
            if (!ModelState.IsValid) return View();
            Category existCategory = _db.Categories.FirstOrDefault(c => c.Name.ToLower().Trim() == createCategoryVM.Name.ToLower().Trim());
            if(existCategory!= null)
            {
                ModelState.AddModelError("Name", "Bu adda kateqoriya movcuddur!");
                return View();
            }
            Category newCategory = new Category
            {
                Name=createCategoryVM.Name
            };
            await _db.Categories.AddAsync(newCategory);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Category category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return View();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null) return NotFound();
            Category dbCategory = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (dbCategory == null) return View();
            Category existCategory = _db.Categories.FirstOrDefault(c => c.Name == category.Name);
            if (existCategory != null)
            {
                if (dbCategory != existCategory)
                {
                    ModelState.AddModelError("Name", "Bu adda kateqoriya movcuddur");
                    return View();
                }
            }
            dbCategory.Name = category.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Category category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
