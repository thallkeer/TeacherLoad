using TeacherLoad.Core.Models;

namespace TeacherLoad.Core.DataInterfaces
{
    public interface IUnitOfWork
    {
        ITeacherService Teachers { get; }
        IGroupService Groups { get; }
        IGroupLoadService GroupLoads { get; }
        IPersonalLoadService PersonalLoads { get; }
        IGenericService<Position> Positions { get; }
        IGenericService<Department> Departments { get; }
        IGenericService<Speciality> Specialities { get; }
        IGenericService<Discipline> Disciplines { get; }
        IGenericService<GroupStudies> GroupStudies { get; }
        IGenericService<IndividualStudies> IndividualStudies { get; }
        IGenericService<ApplicationUser> AppUsers { get;}
        void Save();
    }
}
