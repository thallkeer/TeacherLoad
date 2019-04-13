using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class Discipline
    {
        [ScaffoldColumn(false)]
        public int DisciplineID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Дисциплина")]    
        [RegularExpression(@"^([А-Яа-яЁё\s])+$", ErrorMessage = "Название дисциплины может содержать только буквы русского алфавита!")]        
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Название должно содержать от 2 до 30 символов!")]        
        public string DisciplineName { get; set; }

        public List<GroupLoad> GroupLoads { get; set; }
        public Discipline()
        {
            GroupLoads = new List<GroupLoad>();
        }        
    }
}
