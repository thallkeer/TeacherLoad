using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class Position
    {
        [ScaffoldColumn(false)]
        public int PositionID { get; set; }
        [Required]
        [Display(Name ="Должность")]
        [RegularExpression(@"^([А-Яа-яЁё\s])+$", ErrorMessage = "Название должности может содержать только буквы русского алфавита!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Название должности должно содержать от 3 до 30 символов!")]
        public string PositionName { get; set; }

        public List<Teacher> Teachers { get; set; }

        public Position()
        {
            Teachers = new List<Teacher>();
        }

        public override string ToString()
        {
            return PositionName;
        }
    }
}
