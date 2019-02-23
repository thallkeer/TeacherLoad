using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeacherLoad.Core.Models
{
    public class GroupLoad
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Количество часов")]
        public int VolumeHours { get; set; }
        [Required]
        [Display(Name = "Преподаватель")]
        public int TeacherID { get; set; }
        [Required]
        [Display(Name = "Дисциплина")]
        public int DisciplineID { get; set; }
        [Required]
        [Display(Name = "Номер группы")]
        public string GroupNumber { get; set; }
        [Required]
        [Display(Name = "Вид занятия")]
        public int GroupStudiesID { get; set; }
        [Display(Name = "Преподаватель")]
        public virtual Teacher Teacher { get; set; }
        [Display(Name = "Дисциплина")]
        public virtual Discipline Discipline { get; set; }
        [Display(Name = "Номер группы")]
        public virtual Group Group { get; set; }
        [Display(Name = "Вид занятия")]
        public virtual GroupStudies GroupStudies { get; set; }
    }
}
