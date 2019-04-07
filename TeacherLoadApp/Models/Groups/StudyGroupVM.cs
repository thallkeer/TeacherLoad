using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherLoad.Core.Models;

namespace TeacherLoadApp.Models.Groups
{
    public class StudyGroupVM
    {
        public SelectList Specialities { get; set; }
        public Group Group { get; set; }
    }
}
