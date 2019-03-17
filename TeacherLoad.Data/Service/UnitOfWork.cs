using System;
using System.Data;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TeacherLoadContext context;
        private ITeacherService teacherService;
        private IGroupService groupService;
        private IGroupLoadService groupLoadService;
        private IPersonalLoadService personalLoadService;
        private IGenericService<Position> positionsService;
        private IGenericService<Department> departmentsService;
        private IGenericService<Speciality> specialitiesService;
        private IGenericService<Discipline> disciplinesService;
        private IGenericService<GroupStudies> groupStudiesService;
        private IGenericService<IndividualStudies> individualStudiesService;       

        public UnitOfWork(TeacherLoadContext context)
        {
            this.context = context;
        }

        public ITeacherService Teachers
        {
            get => teacherService = teacherService ?? new TeacherService(context);            
        }

        public IGroupService Groups
        {
            get => groupService = groupService ?? new GroupService(context);
        }

        public IGroupLoadService GroupLoads
        {
            get => groupLoadService = groupLoadService ?? new GroupLoadService(context);
        }

        public IPersonalLoadService PersonalLoads
        {
            get => personalLoadService = personalLoadService ?? new PersonalLoadService(context);
        }

        public IGenericService<Position> Positions
        {
            get => positionsService = positionsService ?? new GenericService<Position>(context);
        }

        public IGenericService<Department> Departments
        {
            get => departmentsService = departmentsService ?? new GenericService<Department>(context);
        }

        public IGenericService<Speciality> Specialities
        {
            get => specialitiesService = specialitiesService ?? new GenericService<Speciality>(context);
        }

        public IGenericService<Discipline> Disciplines
        {
            get => disciplinesService = disciplinesService ?? new GenericService<Discipline>(context);
        }

        public IGenericService<GroupStudies> GroupStudies
        {
            get => groupStudiesService = groupStudiesService ?? new GenericService<GroupStudies>(context);
        }

        public IGenericService<IndividualStudies> IndividualStudies
        {
            get => individualStudiesService = individualStudiesService ?? new GenericService<IndividualStudies>(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
