using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TeacherLoad.Core.Models;

namespace TeacherLoadApp.Models
{
    public class TeacherPersonalLoadVM
    {
       public SelectList PersonalStudies { get; set; }
       public List<GroupingVM<PersonalLoad>> GroupedLoads { get; set; }
       public List<PersonalLoad> PersonalLoads { get; set; }
    }
}
