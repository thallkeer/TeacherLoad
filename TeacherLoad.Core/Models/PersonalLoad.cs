using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherLoad.Core.Models
{
    public class PersonalLoad
    {
        [Required]
        [Display(Name = "Число студентов")]
        [Range(1,40)]
        public int StudentsCount { get; set; }        
        [Required]
        [Display(Name = "Преподаватель")]
        public int TeacherID { get; set; }
        [Required]
        [Display(Name = "Вид занятия")]
        public int IndividualClassID { get; set; }
        [NotMapped]
        [Display(Name ="Объем нагрузки в часах")]
        [ReadOnly(true)]
        public int TotalVolume { get { return IndividualStudies.VolumeByPerson * StudentsCount; } }
        [Display(Name = "Преподаватель")]
        public virtual Teacher Teacher { get; set; }
        [Display(Name = "Вид занятия")]
        public virtual IndividualStudies IndividualStudies { get; set; }       
    }
}
