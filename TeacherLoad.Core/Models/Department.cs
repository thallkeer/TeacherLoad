using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class Department
    {
        [ScaffoldColumn(false)]
        public int DepartmentID { get; set; }

        [Required]
        [Display(Name = "Название кафедры")]
        public string DepartmentName { get; set; }

        public List<Teacher> Teachers { get; set; }

        public override string ToString()
        {
            return DepartmentName;
        }

        public Department()
        {
            Teachers = new List<Teacher>();
        }
    }
}
