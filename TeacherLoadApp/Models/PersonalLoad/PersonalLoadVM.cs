using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeacherLoad.Core.Models;

namespace TeacherLoadApp.Models
{
    public class PersonalLoadVM
    {
        public PersonalLoad PersonalLoad { get; set; }
        public SelectList Teachers { get; set; }
        public SelectList Classes { get; set; }        
    }
}
