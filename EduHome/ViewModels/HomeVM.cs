using EduHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduHome.ViewModels;

namespace EduHome.ViewModels
{
    public class HomeVM
    {
        public HomeBio HomeBio { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
    }
}
