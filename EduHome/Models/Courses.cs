using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AboutCourses { get; set; }
        public string HowToApply { get; set; }
        public string Certification { get; set; }
        public DateTime Starts { get; set; }
        public string Duration { get; set; }
        public string ClassDuration { get; set; }
        public string SkillLevel { get; set; }
        public string Language { get; set; }
        public int Students { get; set; }
        public string Assesments { get; set; }
        public int CourseFee { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public ICollection<CategoryCourses> CategoryCourses { get; set; }
    }
}
