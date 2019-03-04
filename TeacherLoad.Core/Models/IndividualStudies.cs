using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class IndividualStudies
    {
        [Key]
        public int IndividualClassID { get; set; }
        [Required]
        [Display(Name ="Вид занятия")]
        [RegularExpression(@"[А-Яа-я]{1,40}$",
        ErrorMessage = "Вид занятия может содержать только буквы")]
        public string IndividualClassName { get; set; }
        [Required]
        [Display(Name = "Нагрузка на одного человека")]
        [Range(1,32)]
        public int VolumeByPerson { get; set; }
    }
}
