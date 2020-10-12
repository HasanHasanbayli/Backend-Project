  using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string About { get; set; }
        public string Degree { get; set; }
        public string Experience { get; set; }
        public string Hobbies { get; set; }
        public string Faculty { get; set; }
        public string Mail { get; set; }
        public int Call { get; set; }
        public string Skype { get; set; }
        public string Facebook { get; set; }
        public string Pinterest { get; set; }
        public string Vkontakte { get; set; }
        public string Twitter { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public Skill Skill { get; set; }
    }
}
