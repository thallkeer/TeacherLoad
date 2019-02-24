using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeacherLoad.Core.Models
{
   public class IndividualStudies
    {
        [Key]
        public int IndividualClassID { get; set; }
        [Required]
        [Display(Name ="Вид занятия")]
        public string IndividualClassName { get; set; }           
    }
}
