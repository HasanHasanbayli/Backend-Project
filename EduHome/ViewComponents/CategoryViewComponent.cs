using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewComponents
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly AppDbContext _db;
        public CategoryViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Category> model = _db.Categories.ToList();
            return View(await Task.FromResult(model));
        }
    }
}
