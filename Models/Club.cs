using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SocilaMediaProject.Data.Enum;

namespace SocilaMediaProject.Models
{
    public class Club
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Club Title Is Required")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        [ForeignKey("Address")]
        public int? AddressID { get; set; }
        public Address? Address { get; set; }

        [Required(ErrorMessage = "This Field is Requierd")]
        public ClubCategory ClubCategory { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserID { get; set; }
        public AppUser? AppUser { get; set; }

    }
}