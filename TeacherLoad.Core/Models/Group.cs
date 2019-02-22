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
        public string GroupNumber { get; set; }

        public bool FullTime { get; set; }
        [Required]
        [StringLength(30)]
        public string Semester { get; set; }
        [Required]
        public int SpecialityCode { get; set; }

        public virtual Speciality Speciality { get; set; }
    }
}
