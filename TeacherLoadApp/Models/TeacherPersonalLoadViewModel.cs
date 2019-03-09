using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherLoad.Core.Models;

namespace TeacherLoadApp.Models
{
    public class TeacherPersonalLoadViewModel
    {
       public SelectList PersonalStudies { get; set; }
       public List<GroupingViewModel<PersonalLoad>> GroupedLoads { get; set; }
       public List<PersonalLoad> PersonalLoads { get; set; }
    }
}
