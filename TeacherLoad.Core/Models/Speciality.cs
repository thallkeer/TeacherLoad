﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeacherLoad.Core.Models
{
    public class Speciality
    {
        [Key]
        [Display(Name = "Код специальности")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Название специальности")]
        [RegularExpression(@"[А-Яа-я]{1,40}$",
        ErrorMessage = "Название специальности может содержать только буквы")]
        public string SpecialityName { get; set; }

        public List<Group> Groups { get; set; }

        public Speciality()
        {
            Groups = new List<Group>();
        }

        public override string ToString()
        {
            return SpecialityName;
        }
    }
}
