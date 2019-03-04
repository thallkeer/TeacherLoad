using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class GroupLoad
    {
        public enum SemesterType
        {
            [Display(Name = "Осенний")]
            FallSemester,
            [Display(Name = "Весенний")]
            SpringSemester
        }

        public enum StudyTypes
        {
            [Display(Name = "Очное")]
            FullTime,
            [Display(Name = "Заочное")]
            CorrespondenceCourse,
            [Display(Name = "Вечернее")]
            Evening
        }
                
        [Required]
        [Display(Name = "Количество часов")]
        [Range(1,32)]
        public int VolumeHours { get; set; }        
        [Required]
        [Display(Name = "Семестр")]
        public SemesterType Semester { get; set; }
        [Required]
        [Display(Name = "Вид обучения")]
        public StudyTypes StudyType { get; set; }
        [Required]
        [Display(Name = "Курс")]
        [Range(1,4)]
        public int StudyYear { get; set; }
        [Required]
        [Display(Name = "Преподаватель")]
        public int TeacherID { get; set; }
        [Required]
        [Display(Name = "Дисциплина")]
        public int DisciplineID { get; set; }
        [Required]
        [Display(Name = "Номер группы")]
        public string GroupNumber { get; set; }
        [Required]
        [Display(Name = "Вид занятия")]
        public int GroupStudiesID { get; set; }
        [Display(Name = "Преподаватель")]
        public virtual Teacher Teacher { get; set; }
        [Display(Name = "Дисциплина")]
        public virtual Discipline Discipline { get; set; }
        [Display(Name = "Номер группы")]
        public virtual Group Group { get; set; }
        [Display(Name = "Вид занятия")]
        public virtual GroupStudies GroupStudies { get; set; }
    }
}
