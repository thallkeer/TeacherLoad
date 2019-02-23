using System;
using System.Collections.Generic;
using System.Text;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Core.DataInterfaces
{
    public interface IGroupService : IService<Group>
    {
        Group GetById(string id);
    }
}
