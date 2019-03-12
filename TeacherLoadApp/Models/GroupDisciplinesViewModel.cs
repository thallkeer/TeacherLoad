using System.ComponentModel.DataAnnotations;

namespace TeacherLoadApp.Models
{
    public class GroupDisciplinesViewModel
    {        
        [Display(Name = "Дисциплина")]
        public string Discipline { get; set; }
        [Display(Name = "Преподаватель")]
        public string TeacherName { get; set; }
        [Display(Name = "Вид занятий")]
        public string ClassType { get; set; }
    }
}
