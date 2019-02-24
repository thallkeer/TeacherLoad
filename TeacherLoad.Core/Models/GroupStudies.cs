using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeacherLoad.Core.Models
{
    public class GroupStudies
    {
        [Key]
        public int GroupClassID { get; set; }
        [Required]
        [Display(Name = "Вид занятия")]
        public string GroupClassName { get; set; }
    }
}
