using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoadApp.Models
{
    public class UserWithRoleViewModel
    {
        public string UserId { get; set; }
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        [Display(Name = "Роли пользователя")]
        public IList<string> UserRoles { get; set; }
        public UserWithRoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
