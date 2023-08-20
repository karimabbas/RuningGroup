using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocilaMediaProject.Models
{
    public class Address
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Street Requierd")]
        public string? Street { get; set; }
        [Required(ErrorMessage = " City is Requierd")]
        public string? City { get; set; }
        [Required(ErrorMessage = " State is Requierd")]
        public string? State { get; set; }
        // [Range(3, 6)]
        public int ZipCode { get; set; }
        public AppUser? AppUser { get; set; }


    }
}