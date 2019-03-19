using System.Collections.Generic;

namespace TeacherLoadApp.Models
{
    public class GroupingVM<T>
    {
        public string Key { get; set; }
        public List<T> Values { get; set; }
    }
}
