using System;
using System.ComponentModel.DataAnnotations;

namespace AppoinmentScheduling.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name ="Remember me?")]

        public bool RememberMe { get; set; }

    }

}