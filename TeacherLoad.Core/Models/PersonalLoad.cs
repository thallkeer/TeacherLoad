using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherLoad.Core.Models
{
    public class PersonalLoad
    {
        [ScaffoldColumn(false)]
        public int PersonalLoadID { get; set; }
        [Required]
        [Display(Name = "Число студентов")]
        [Range(1,32)]
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
        public int TotalVolume
        {
            get
            {
                if (IndividualStudies != null)
                    return IndividualStudies.VolumeByPerson * StudentsCount;
                return StudentsCount;
            }
        }
        [Display(Name = "Преподаватель")]
        public virtual Teacher Teacher { get; set; }
        [Display(Name = "Вид занятия")]
        public virtual IndividualStudies IndividualStudies { get; set; }       
    }
}
