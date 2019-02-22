using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeacherLoad.Core.Models
{
   public class IndividualStudies
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        public string IndividualClassType { get; set; }
        [Required]
        public int VolumeByPerson { get; set; }
    }
}
