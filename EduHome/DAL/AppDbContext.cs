using EduHome.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<HomeBio> HomeBios { get; set; }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryCourses> CategoryCourses { get; set; }
        public DbSet<CategoryEvent> CategoryEvents { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Spiker> Spikers { get; set; }
        public DbSet<SpikerEvent> SpikerEvents { get; set; }
    }
}
