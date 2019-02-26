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
    public class GroupService : GenericCatalogService<Group>, IGroupService
    {
        public GroupService(TeacherLoadContext context) : base(context)
        { }

        public override IEnumerable<Group> GetAll()
        {
            return Get(includeProperties: "Speciality");
        }
    }
}
