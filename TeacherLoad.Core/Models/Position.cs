using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class Position
    {
        [ScaffoldColumn(false)]
        public int PositionID { get; set; }
        [Required]
        [Display(Name ="Должность")]
        [RegularExpression(@"[А-Яа-я]{1,40}$",
        ErrorMessage = "Должность может содержать только буквы")]
        public string PositionName { get; set; }

        public List<Teacher> Teachers { get; set; }

        public Position()
        {
            Teachers = new List<Teacher>();
        }

        public override string ToString()
        {
            return PositionName;
        }
    }
}
