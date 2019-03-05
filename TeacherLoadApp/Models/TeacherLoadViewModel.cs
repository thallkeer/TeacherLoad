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
        public List<GroupLoad> GroupLoads { get; set; }
    }
}
