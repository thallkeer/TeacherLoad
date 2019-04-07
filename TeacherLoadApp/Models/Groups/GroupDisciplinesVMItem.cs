using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoadApp.Models
{
    public class GroupDisciplinesVMItem
    {
        [Display(Name = "Дисциплина")]
        public string Discipline { get; set; }
        [Display(Name = "Преподаватель")]
        public string TeacherName { get; set; }
        [Display(Name = "Вид занятий")]
        public string ClassType { get; set; }
    }
}
