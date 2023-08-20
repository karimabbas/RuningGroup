using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocilaMediaProject.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? CityName { get; set; }
        public string? StateCode { get; set; }
        public int Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? County { get; set; }

    }
}