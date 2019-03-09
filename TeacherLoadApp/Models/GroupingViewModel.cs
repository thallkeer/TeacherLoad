using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherLoad.Core.Models;

namespace TeacherLoadApp.Models
{
    public class GroupingViewModel<T>
    {
        public string Key { get; set; }
        public List<T> Values { get; set; }
    }
}
