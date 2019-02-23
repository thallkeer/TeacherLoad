using System;
using System.Collections.Generic;
using System.Text;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Core.DataInterfaces
{
    public interface IUnitOfWork
    {
        ITeacherService Teachers { get; }
        IGroupService Groups { get; }
        IGroupLoadService GroupLoads { get; }
        IPersonalLoadService PersonalLoads { get; }
        IGenericCatalogService<Position> Positions { get; }
        IGenericCatalogService<Department> Departments { get; }
        IGenericCatalogService<Speciality> Specialities { get; }
        IGenericCatalogService<Discipline> Disciplines { get; }
        IGenericCatalogService<GroupStudies> GroupStudies { get; }
        IGenericCatalogService<IndividualStudies> IndividualStudies { get; }
    }
}
