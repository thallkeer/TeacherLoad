using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherLoad.Core.Models;

namespace TeacherLoadApp.Models
{
    public class GroupsIndexVM
    {
        public Group Instance { get; set; }
        public SelectList Years { get; set; }
        public List<GroupingVM<Group>> GroupedStudyGroups {get;set;}
    }
}
