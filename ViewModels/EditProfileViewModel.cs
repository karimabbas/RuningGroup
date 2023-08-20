using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.ViewModels
{
    public class EditProfileViewModel
    {
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public string? ProfileImageUrl { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public int? AddressID { get; set; }
        public Address? Address { get; set; }
        public IFormFile? Image { get; set; }

    }
}