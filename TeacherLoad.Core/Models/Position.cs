using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeacherLoad.Core.Models
{
    public class Position
    {
        [ScaffoldColumn(false)]
        public int PositionID { get; set; }
        [Required]
        public string PositionName { get; set; }

        public override string ToString()
        {
            return PositionName;
        }
    }
}
