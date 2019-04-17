using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherLoad.Core.Models
{
    public class Teacher
    {
        [ScaffoldColumn(false)]
        public int TeacherID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Имя")]
        [RegularExpression(@"^([А-Яа-яЁё\s])+$", ErrorMessage = "Имя может содержать только буквы русского алфавита!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 50 символов!")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Фамилия")]
        [RegularExpression(@"^([А-Яа-яЁё\s])+$", ErrorMessage = "Фамилия может содержать только буквы русского алфавита!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия должно содержать от 2 до 50 символов!")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Отчество")]
        [RegularExpression(@"^([А-Яа-яЁё\s])+$", ErrorMessage = "Отчество может содержать только буквы русского алфавита!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Отчество должно содержать от 2 до 50 символов!")]
        public string Patronym { get; set; }
        [Required]
        [Display(Name = "Кафедра")]
        public int DepartmentID { get; set; }
        [Required]
        [Display(Name = "Должность")]
        public int PositionID { get; set; }
        [Display(Name = "Кафедра")]
        public virtual Department Department { get; set; }
        [Display(Name = "Должность")]
        public virtual Position Position { get; set; }
       
        public virtual List<GroupLoad> GroupLoads { get; set; }
        public virtual List<PersonalLoad> PersonalLoads { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return $"{LastName}  {FirstName}  {Patronym}"; }
        }

        public override string ToString()
        {
            return FullName;
        }

        public Teacher()
        {
            GroupLoads = new List<GroupLoad>();
            PersonalLoads = new List<PersonalLoad>();
        }
    }
}
