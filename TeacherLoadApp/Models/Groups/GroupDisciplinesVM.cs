using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherLoadApp.Models.Groups
{
    public class GroupDisciplinesVM
    {
        public SelectList Groups { get; set; }
        public List<GroupDisciplinesVMItem> GroupDisciplines { get; set; }
    }
}
