using System;
using System.Collections.Generic;
using System.Text;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Core.DataInterfaces
{
    public interface IGroupLoadService : IService<GroupLoad>
    {
        List<GroupLoad> GetDisciplinesByGroup(string groupNumber);
    }
}
