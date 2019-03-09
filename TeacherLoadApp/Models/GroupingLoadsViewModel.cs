using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherLoad.Core.Models;

namespace TeacherLoadApp.Models
{
    public class GroupingViewModel
    {
        public string Key { get; set; }
        public List<GroupLoad> Values { get; set; }
    }
}
