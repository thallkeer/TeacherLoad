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
        public int StudentsCount { get; set; }
        [Required]
        public int TeacherID { get; set; }
        [Required]
        public int IndividualClassTypeID { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual IndividualStudies IndividualStudies { get; set; }
    }
}
