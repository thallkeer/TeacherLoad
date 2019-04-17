using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherLoad.Core.Models
{
    [Table("StudyGroup")]
    public class Group
    {
        [Key]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Номер группы")]
        [RegularExpression(@"[0-9]{4}$",
        ErrorMessage = "Номер группы должен состоять из четырех цифр")]
        public string GroupNumber { get; set; }      
        [Required]
        [Display(Name = "Количество студентов")]
        [Range(1,25)]
        public int StudentsCount { get; set; }
        [Required]
        [Display(Name = "Специальность")]
        public string SpecialityCode { get; set; }
        [Display(Name = "Специальность")]
        public virtual Speciality Speciality { get; set; }
        [Display(Name = "Курс")]
        public int StudyYear
        {
            get
            {
                int year;
                int.TryParse(GroupNumber[1].ToString(), out year); //second digit in group number is the study year                 
                return year;
            }
        }
        public string GroupWithSpeciality
        {
            get
            {
                return $"{SpecialityCode}   {GroupNumber}";
            }
        }
        public override string ToString()
        {
            return GroupWithSpeciality;
        }
    }
}
