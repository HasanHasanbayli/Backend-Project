using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryCourses> CategoryCourses { get; set; }
        public ICollection<CategoryEvent> CategoryEvents { get; set; }
    }
}
