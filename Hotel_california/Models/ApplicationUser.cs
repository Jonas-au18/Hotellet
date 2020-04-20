using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Hotel_california.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        public string Name { get; set; }
    }
}