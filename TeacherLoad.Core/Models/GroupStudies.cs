using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class GroupStudies
    {
        [Key]
        public int GroupClassID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Вид занятия")]
        [RegularExpression(@"^([А-Яа-яЁё\s])+$", ErrorMessage = "Вид группового занятия может содержать только буквы русского алфавита!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Вид группового занятия должен содержать от 3 до 50 символов!")]
        public string GroupClassName { get; set; }

        public virtual List<GroupLoad> GroupLoads { get; set; }
    }
}
