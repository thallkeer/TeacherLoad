using TeacherLoad.Core.Models;

namespace TeacherLoad.Core.DataInterfaces
{
    public interface IPersonalLoadService : IGenericService<PersonalLoad>
    {
        //Получить нагрузку по составному ключу
        PersonalLoad GetByKeys(int teacherID,int classID);
    }
}
