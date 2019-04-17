using System.Collections.Generic;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class PersonalLoadService : GenericService<PersonalLoad>,IPersonalLoadService
    {
        public PersonalLoadService(TeacherLoadContext context): base(context)
        {}             
    }
}
