using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using SocilaMediaProject.Data.Enum;

namespace SocilaMediaProject.Models
{
    public class Race
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Race Title is Requierd")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public DateTime? StartTime { get; set; }
        public int? EntryFee { get; set; }
        public string? Website { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Contact { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address? Address { get; set; }
        public RaceCategory RaceCategory { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }


    }

}