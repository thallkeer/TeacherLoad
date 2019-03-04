using System.Collections.Generic;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Core.DataInterfaces
{
    public interface IGroupLoadService : IGenericCatalogService<GroupLoad>
    {
        IEnumerable<GroupLoad> GetDisciplinesByGroup(string groupNumber);
    }
}
