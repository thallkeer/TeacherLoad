using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class Position
    {
        [ScaffoldColumn(false)]
        public int PositionID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name ="Должность")]
        [RegularExpression(@"^([А-Яа-яЁё\s])+$", ErrorMessage = "Название должности может содержать только буквы русского алфавита!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Название должности должно содержать от 3 до 50 символов!")]
        public string PositionName { get; set; }

        public virtual List<Teacher> Teachers { get; set; }
                
        public override string ToString()
        {
            return PositionName;
        }
    }
}
