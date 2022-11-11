using System;
using Microsoft.AspNetCore.Identity;

namespace AppoinmentScheduling.Models
{
    public class ApplicationUser:IdentityUser
    {
        public String Name { get; set; }
    }
}

