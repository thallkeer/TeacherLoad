using System.Collections.Generic;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class PersonalLoadService : GenericCatalogService<PersonalLoad>,IPersonalLoadService
    {
        public PersonalLoadService(TeacherLoadContext context): base(context)
        {}

        public override IEnumerable<PersonalLoad> GetAll()
        {
            return Get(includeProperties: "Teacher,IndividualStudies");
        }

        public PersonalLoad GetByKeys(int teacherID, int classID)
        {
            return dbSet.Find(teacherID,classID);
        }
    }
}
