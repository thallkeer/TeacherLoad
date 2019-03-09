using System.Collections.Generic;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Core.DataInterfaces
{
    public interface IGroupLoadService : IGenericService<GroupLoad>
    {
        /// <summary>
        /// Получить список дисциплин, читаемых преподавателями
        /// для указанной группы
        /// </summary>
        /// <param name="groupNumber">Номер группы</param>
        /// <returns></returns>
        IEnumerable<GroupLoad> GetDisciplinesByGroup(string groupNumber);
        /// <summary>
        /// Получить групповую нагрузку по указанному преподавателю
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        IEnumerable<GroupLoad> GetByTeacher(int teacherID);
    }
}
