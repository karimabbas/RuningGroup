using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocilaMediaProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string? ConfirmPassword { get; set; }

    }
}