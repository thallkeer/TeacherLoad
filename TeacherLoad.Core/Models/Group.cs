using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeacherLoad.Core.Models
{
    [Table("StudyGroup")]
    public class Group
    {
        [Key]
        [Display(Name = "Номер группы")]
        public string GroupNumber { get; set; }
        [Display(Name = "Дневное обучение")]
        public bool FullTime { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "Семестр")]
        public string Semester { get; set; }
        [Required]
        [Display(Name = "Специальность")]
        public string SpecialityCode { get; set; }
        [Display(Name = "Специальность")]
        public virtual Speciality Speciality { get; set; }
    }
}
