using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class Discipline
    {
        [ScaffoldColumn(false)]
        public int DisciplineID { get; set; }
        [Required]
        [Display(Name = "Дисциплина")]
        [RegularExpression(@"[А-Яа-я]{1,50}$",
        ErrorMessage = "Название дисциплины может содержать только буквы")]
        public string DisciplineName { get; set; }

        public List<GroupLoad> GroupLoads { get; set; }
        public Discipline()
        {
            GroupLoads = new List<GroupLoad>();
        }
    }
}
