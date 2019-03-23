using System.Collections.Generic;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Core.DataInterfaces
{
    public interface ITeacherService : IGenericService<Teacher>
    {
        /// <summary>
        /// Загрузить всех преподавателей без их навигационных свойств
        /// </summary>
        /// <returns></returns>
        IEnumerable<Teacher> GetAllForSelectList();
    }
}
