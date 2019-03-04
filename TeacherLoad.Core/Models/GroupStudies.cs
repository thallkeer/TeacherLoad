using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class GroupStudies
    {
        [Key]
        public int GroupClassID { get; set; }
        [Required]
        [Display(Name = "Вид занятия")]
        [RegularExpression(@"[А-Яа-я]{1,40}$",
        ErrorMessage = "Вид занятия может содержать только буквы")]
        public string GroupClassName { get; set; }
    }
}
