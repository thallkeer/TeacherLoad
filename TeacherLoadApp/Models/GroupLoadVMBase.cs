using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherLoad.Core.Models;

namespace TeacherLoadApp.Models
{
    public class GroupLoadVMBase
    {
        /// <summary>
        /// Instance for simplify access to properties
        /// </summary>
        public GroupLoad  GroupLoad { get; set; }
        public SelectList Teachers { get; set; }
        public SelectList GroupStudies { get; set; }
        public SelectList Semesters { get; set; }
        public SelectList StudyTypes { get; set; }
        public SelectList StudyYears { get; set; }
    }
}
