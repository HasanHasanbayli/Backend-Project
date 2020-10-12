using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public ICollection<SpikerEvent> SpikerEvents { get; set; }
        public ICollection<CategoryEvent> CategoryEvents { get; set; }
    }
}
