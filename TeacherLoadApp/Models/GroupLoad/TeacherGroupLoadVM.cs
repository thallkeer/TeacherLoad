using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TeacherLoad.Core.Models;

namespace TeacherLoadApp.Models
{
    public class TeacherGroupLoadVM : GroupLoadVMBase
    {         
        public List<GroupingVM<GroupLoad>> GroupedLoads { get; set; }       
    }
}
