using System;
using System.Collections.Generic;
using System.Text;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Core.DataInterfaces
{
    public interface IUnitOfWork
    {
        ITeacherService TeacherService { get; }
        IGroupService GroupService { get; }
        IGroupLoadService GroupLoadService { get; }
        IPersonalLoadService PersonalLoadService { get; }
        IGenericCatalogService<Position> PositionsService { get; }
        IGenericCatalogService<Department> DepartmentsService { get; }
        IGenericCatalogService<Speciality> SpecialitiesService { get; }
        IGenericCatalogService<Discipline> DisciplinesService { get; }
    }
}
