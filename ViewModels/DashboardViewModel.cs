using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.ViewModels
{
    public class DashboardViewModel
    {
        public List<Club>? Clubs { get; set; }

        public List<Race>? Races { get; set; }
    }
}