using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherLoad.Core.Models;

namespace TeacherLoadApp.Models
{
    public class TeacherLoadViewModel
    {
        public List<TeachersListViewModel> TeachersList { get; set; }
        public int SelectedTeacher { get; set; }
        public SelectList GroupStudies { get; set; }
        public SelectList Semesters { get; set; }
        public SelectList StudyTypes { get; set; }
        public SelectList StudyYears { get; set; }
        public List<GroupingViewModel<GroupLoad>> GroupedLoads { get; set; }       
    }
}
