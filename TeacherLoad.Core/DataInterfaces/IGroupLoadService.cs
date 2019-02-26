using System;
using System.Collections.Generic;
using System.Text;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Core.DataInterfaces
{
    public interface IGroupLoadService : IGenericCatalogService<GroupLoad>
    {
        IEnumerable<GroupLoad> GetDisciplinesByGroup(string groupNumber);
    }
}
