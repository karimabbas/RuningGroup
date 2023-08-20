using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocilaMediaProject.ViewModels
{
    public class HomeUserCreateViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        public int? ZipCode { get; set; }
    }
}