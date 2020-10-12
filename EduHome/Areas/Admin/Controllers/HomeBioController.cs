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

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeBioController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public HomeBioController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            HomeBio bio = _db.HomeBios.FirstOrDefault();
            return View(bio);
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            HomeBio bio = _db.HomeBios.FirstOrDefault(p => p.Id == id);
            if (bio == null) return NotFound();
            return View(bio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(HomeBio bio)
        {
            HomeBio dbHomeBio = _db.HomeBios.FirstOrDefault(p => p.Id == bio.Id);
            if (dbHomeBio == null) return NotFound();
            if (bio.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    return View();
                }
                if (!bio.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Zehmet olmasa shekil formati sechin");
                    return View();
                }
                if (bio.Photo.MaxLength(2000))
                {
                    ModelState.AddModelError("Photo", "Shekilin olchusu max 2mg ola biler");
                    return View();
                }
                string path = Path.Combine("img", "logo");
                Helper.DeleteImage(_env.WebRootPath, path, dbHomeBio.HeaderLogo);
                string fileName = await bio.Photo.SaveImg(_env.WebRootPath, path);
                dbHomeBio.HeaderLogo = fileName;
            }
            dbHomeBio.Adress = bio.Adress;
            dbHomeBio.Telephone = bio.Telephone;
            dbHomeBio.Email = bio.Email;
            dbHomeBio.WebSite = bio.WebSite;
            dbHomeBio.Facebook = bio.Facebook;
            dbHomeBio.Instagram = bio.Instagram;
            dbHomeBio.Twitter = bio.Twitter;
            dbHomeBio.Youtube = bio.Youtube;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
