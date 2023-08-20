using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SocilaMediaProject.Data.Enum;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.ViewModels
{
    public class ClubViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Club Title Is Required")]
        [MaxLength(12),MinLength(7)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Address? Address { get; set; }
        public int? AddressID { get; set; }

        public string? Imagepath {get;set;}
        public IFormFile? Image { get; set; }
        public ClubCategory ClubCategory { get; set; }
        public string? AppUserId { get; set; }
    }
}