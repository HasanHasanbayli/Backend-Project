using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.DAL;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly AppDbContext _db;
        public SkillController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Skills.Include(s=>s.Teacher).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Teachers = _db.Teachers.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Skill skill)
        {
            ViewBag.Teachers = _db.Teachers.ToList();
            if (!ModelState.IsValid) return View();

            Skill existSkill = _db.Skills.FirstOrDefault(s => s.TeacherId == skill.TeacherId);
            if (existSkill != null)
            {
                ModelState.AddModelError("TeacherId", $" {existSkill.Teacher.FullName} -in skilleri movcuddur,update edin zehmet olmasa");
                return View();
            }
            Skill newSkill = new Skill
            {
                Language = skill.Language,
                Design = skill.Design,
                Development = skill.Development,
                Innovation = skill.Innovation,
                Communication = skill.Communication,
                TeamLeader = skill.TeamLeader,
                TeacherId = skill.TeacherId
            };
            _db.Skills.Add(newSkill);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Skill skill = _db.Skills.FirstOrDefault(p => p.Id == id);
            if (skill == null) return NotFound();
            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Skill skill)
        {
            Skill dbSkill = _db.Skills.FirstOrDefault(p => p.Id == skill.Id);
            if (dbSkill == null) return NotFound();
            dbSkill.Language = skill.Language;
            dbSkill.TeamLeader = skill.TeamLeader;
            dbSkill.Innovation = skill.Innovation;
            dbSkill.Design = skill.Design;
            dbSkill.Development = skill.Development;
            dbSkill.Communication = skill.Communication;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null) return NotFound();
        //    Skill skill = await _db.Skills.FindAsync(id);
        //    if (skill == null) return NotFound();
        //    return View(skill);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        //public async Task<IActionResult> DeleteSkill(int? id)
        //{
        //    if (id == null) return NotFound();
        //    Skill dbSkill = await _db.Skills.FindAsync(id);
        //    if (dbSkill == null) return NotFound();
        //    _db.Skills.Remove(dbSkill);
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
