using System.Collections.Generic;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TeacherLoad.Data.Service
{
    public class TeacherService : GenericService<Teacher>,ITeacherService
    {
        public TeacherService(TeacherLoadContext context) : base(context)
        {}


        public override IEnumerable<Teacher> GetAll()
        {
            return Get(includeProperties: "Department,Position");
        }

        public override Teacher GetByID(object id)
        {
            return dbSet.Include(t => t.Department)
                        .Include(t => t.Position)
                        .Include(t => t.GroupLoads)
                        .Include(t => t.PersonalLoads).FirstOrDefault();
        }
    }
}
