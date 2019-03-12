using System.Collections.Generic;

namespace TeacherLoadApp.Models
{
    public class GroupingViewModel<T>
    {
        public string Key { get; set; }
        public List<T> Values { get; set; }
    }
}
