using System.Collections.Generic;
using System.Linq;
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

        public override Teacher GetByID(object id)
        {
            return Get(t => t.TeacherID == (int)id,includeProperties:"Department,Position").FirstOrDefault();
        }
    }
}
