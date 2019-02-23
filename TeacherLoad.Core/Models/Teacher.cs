using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeacherLoad.Core.Models
{
    public class Teacher
    {
        [ScaffoldColumn(false)]
        public int TeacherID { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Отчество")]
        public string Patronym { get; set; }
        [Required]
        [Display(Name = "Кафедра")]
        public int DepartmentID { get; set; }
        [Required]
        [Display(Name = "Должность")]
        public int PositionID { get; set; }
        [Display(Name = "Кафедра")]
        public virtual Department Department { get; set; }
        [Display(Name = "Должность")]
        public virtual Position Position { get; set; }
       
        public List<GroupLoad> GroupLoads { get; set; }

        public string FullName
        {
            get { return $"{LastName}  {FirstName}  {Patronym}"; }
        }

        public override string ToString()
        {
            return FullName;
        }

        public Teacher()
        {
            GroupLoads = new List<GroupLoad>();
        }
    }
}
