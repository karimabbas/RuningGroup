using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SocilaMediaProject.Models
{
    public class AppUser : IdentityUser
    {

        public int? Pace { get; set; }
        public int? Mileage { get; set; }

        // public string? UserName { get; set; }
        public string? ProfileImageUrl { get; set; }

        [ForeignKey("Address")]
        public int? AddresID { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club>? Clubs { get; set; }
        public ICollection<Race>? Races { get; set; }
        [NotMapped]

        public string? Role { get; set; }
        [NotMapped]

        public string? RoleId { get; set; }
        [NotMapped]

        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}