using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class TeacherService : GenericCatalogService<Teacher>,ITeacherService
    {
        public TeacherService(TeacherLoadContext context) : base(context)
        {}


        public override IEnumerable<Teacher> GetAll()
        {
            return Get(includeProperties: "Department,Position");
        }
    }
}
