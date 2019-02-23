using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeacherLoad.Core.Models
{
    public class GroupStudies
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Вид занятия")]
        public string ClassType { get; set; }
    }
}
