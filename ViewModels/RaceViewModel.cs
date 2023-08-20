using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SocilaMediaProject.Data.Enum;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.ViewModels
{
    public class RaceViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Race Title is Requierd")]
        public string? Title { get; set; }
        public string? Description { get; set; }

        public string? Image { get; set; }
        public IFormFile? Formfile { get; set; }
        public DateTime? StartTime { get; set; }
        public int? EntryFee { get; set; }
        public string? Website { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Contact { get; set; }

        public Address? Address { get; set; }

        [Required(ErrorMessage = "This Field is Requierd")]
        public RaceCategory RaceCategory { get; set; }

        public string? AppUserID { get; set; }
        // public AppUser? AppUser { get; set; }

    }
}