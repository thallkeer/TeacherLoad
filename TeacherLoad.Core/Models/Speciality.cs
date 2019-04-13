using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherLoad.Core.Models
{
    public class Speciality
    {
        public enum EducationLevels
        {
            Bachelor,
            Specialist,
            Master
        }

        [Key]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Код специальности")]
        [RegularExpression(@"^([0-9]{2}.[0-9]{2}.[0-9].)+$", ErrorMessage = "Код специальности должен быть в формате xx.xx.xx, где x - цифра от 0 до 9")]
        public string Code { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Название специальности")]
        [RegularExpression(@"^([А-Яа-яЁё\s])+$", ErrorMessage = "Название специальности может содержать только буквы!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Название должно содержать от 3 до 30 символов!")]
        public string SpecialityName { get; set; }
        [Required]
        [Display(Name = "Уровень образования")]
        public EducationLevels EducationLevel { get; set; }

        public List<Group> Groups { get; set; }

        public Speciality()
        {
            Groups = new List<Group>();
        }
        [NotMapped]
        public string NameWithCode
        {
            get
            {
                return $"{Code} {SpecialityName}";
            }
        }

        public override string ToString()
        {
            return SpecialityName;
        }
    }
}
