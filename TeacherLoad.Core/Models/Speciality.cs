using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeacherLoad.Core.Models
{
    public class Speciality
    {
        [Key]
        public int Code { get; set; }
        [Required]
        public string SpecialityName { get; set; }
    }
}
