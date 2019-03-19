using Microsoft.AspNetCore.Mvc.Rendering;


namespace TeacherLoadApp.Models
{
    public class GroupLoadVM : GroupLoadVMBase
    {         
        public SelectList Disciplines { get; set; }
        public SelectList Groups { get; set; }       
        public SelectList Specialities { get; set; }
    }
}
