using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoadApp.Models
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Логин")]
        public string Username { get; set; }
               
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }     
        
        public List<IdentityRole> Roles { get; set; }

        public EditUserViewModel()
        {
            Roles = new List<IdentityRole>();
        }
    }
}
