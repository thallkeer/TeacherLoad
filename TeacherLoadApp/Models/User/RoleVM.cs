using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherLoadApp.Models.User
{
    public class RoleVM
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Имя роли")]
        [RegularExpression(@"^([А-Яа-яЁё\s])+$", ErrorMessage = "Имя роли может содержать только буквы русского алфавита!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Имя роли должно содержать от 3 до 30 символов!")]
        public string RoleName { get; set; }
    }
}
