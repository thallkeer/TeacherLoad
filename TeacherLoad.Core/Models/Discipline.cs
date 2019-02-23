using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeacherLoad.Core.Models
{
    public class Discipline
    {
        [ScaffoldColumn(false)]
        public int DisciplineID { get; set; }
        [Required]
        [Display(Name = "Дисциплина")]
        public string DisciplineName { get; set; }

        public List<GroupLoad> GroupLoads { get; set; }
        public Discipline()
        {
            GroupLoads = new List<GroupLoad>();
        }
    }
}
