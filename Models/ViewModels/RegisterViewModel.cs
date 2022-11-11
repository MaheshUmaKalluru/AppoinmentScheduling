using System;
using System.ComponentModel.DataAnnotations;

namespace AppoinmentScheduling.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        
        [StringLength(100,ErrorMessage ="the {0} must be at leasr {2} characters long", MinimumLength =6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirm password")]
        [Compare("Password", ErrorMessage ="the password and confrim password dosn't match")]
        public string ConfirmPassowrd { get; set; }

        [Required]
        [Display(Name="Role Name")]

        public string RoleName { get; set; }
    }
}

