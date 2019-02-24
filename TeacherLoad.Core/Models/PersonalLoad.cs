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
        [Display(Name = "Нагрузка на одного человека")]
        public int VolumeByPerson { get; set; }
        [Required]
        [Display(Name = "Преподаватель")]
        public int TeacherID { get; set; }
        [Required]
        [Display(Name = "Вид занятия")]
        public int IndividualClassID { get; set; }
        [Display(Name = "Преподаватель")]
        public virtual Teacher Teacher { get; set; }
        [Display(Name = "Вид занятия")]
        public virtual IndividualStudies IndividualStudies { get; set; }
    }
}
