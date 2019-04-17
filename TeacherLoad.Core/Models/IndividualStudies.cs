using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class IndividualStudies
    {
        [Key]
        public int IndividualClassID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name ="Вид занятия")]
        [RegularExpression(@"^([А-Яа-яЁё\s])+$", ErrorMessage = "Вид индивидуальных занятий может содержать только буквы русского алфавита!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Название вида индивидуальных занятий должно содержать от 2 до 50 символов!")]
        public string IndividualClassName { get; set; }
        [Required]
        [Display(Name = "Нагрузка на одного человека")]
        [Range(1,32)]
        public int VolumeByPerson { get; set; }

        public virtual List<PersonalLoad> PersonalLoads { get; set; }
    }
}
