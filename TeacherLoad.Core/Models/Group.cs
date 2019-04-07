using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherLoad.Core.Models
{
    [Table("StudyGroup")]
    public class Group
    {
        [Key]
        [Display(Name = "Номер группы")]
        [RegularExpression(@"[0-9]{1,4}$",
        ErrorMessage = "Номер группы должен состоять из четырех цифр")]
        public string GroupNumber { get; set; }        
        [Display(Name = "Количество студентов")]
        [Range(1,40)]
        public int StudentsCount { get; set; }
        [Required]
        [Display(Name = "Специальность")]
        public string SpecialityCode { get; set; }
        [Display(Name = "Специальность")]
        public virtual Speciality Speciality { get; set; }
        [NotMapped]
        [Display(Name = "Курс")]
        public int StudyYear
        {
            get
            {
                return int.Parse(GroupNumber[1].ToString()); //second digit in group number is the study year 
            }
        }
        public string GroupWithSpeciality
        {
            get
            {
                return $"{SpecialityCode} {GroupNumber}";
            }
        }
        public override string ToString()
        {
            return GroupWithSpeciality;
        }
    }
}
