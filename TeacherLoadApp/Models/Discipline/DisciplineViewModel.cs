using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherLoad.Core.Models;


namespace TeacherLoadApp.Models.Discipline
{
    public class DisciplineViewModel
    {
       public TeacherLoad.Core.Models.Discipline Discipline { get; set; }
        
       public List<TeacherLoad.Core.Models.Discipline> Disciplines { get; set; }
    }
}
