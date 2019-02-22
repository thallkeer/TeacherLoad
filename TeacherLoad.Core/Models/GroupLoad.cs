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
        public int VolumeHours { get; set; }
        [Required]
        public int TeacherID { get; set; }
        [Required]
        public int DisciplineID { get; set; }
        [Required]
        public string GroupNumber { get; set; }
        [Required]
        public int ClassTypeID { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual Group Group { get; set; }
        public virtual GroupStudies GroupStudies { get; set; }
    }
}
