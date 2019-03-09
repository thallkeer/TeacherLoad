using System.Collections.Generic;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class GroupService : GenericService<Group>, IGroupService
    {
        public GroupService(TeacherLoadContext context) : base(context)
        { }

        public override IEnumerable<Group> GetAll()
        {
            return Get(includeProperties: "Speciality");
        }
    }
}
