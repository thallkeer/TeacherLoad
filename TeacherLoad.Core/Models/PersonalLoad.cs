using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeacherLoad.Core.Models
{
    public class PersonalLoad
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Число студентов")]
        public int StudentsCount { get; set; }
        [Required]
        [Display(Name = "Преподаватель")]
        public int TeacherID { get; set; }
        [Required]
        [Display(Name = "Вид занятия")]
        public int IndividualClassTypeID { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual IndividualStudies IndividualStudies { get; set; }
    }
}
