using System;
using System.Collections.Generic;
using System.Text;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private TeacherLoadContext context;
        private ITeacherService teacherService;
        private IGroupService groupService;
        private IGroupLoadService groupLoadService;
        private IPersonalLoadService personalLoadService;
        private IGenericCatalogService<Position> positionsService;
        private IGenericCatalogService<Department> departmentsService;
        private IGenericCatalogService<Speciality> specialitiesService;
        private IGenericCatalogService<Discipline> disciplinesService;
        private IGenericCatalogService<GroupStudies> groupStudiesService;
        private IGenericCatalogService<IndividualStudies> individualStudiesService;

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

        public IGenericCatalogService<Position> Positions
        {
            get => positionsService = positionsService ?? new GenericCatalogService<Position>(context);
        }

        public IGenericCatalogService<Department> Departments
        {
            get => departmentsService = departmentsService ?? new GenericCatalogService<Department>(context);
        }

        public IGenericCatalogService<Speciality> Specialities
        {
            get => specialitiesService = specialitiesService ?? new GenericCatalogService<Speciality>(context);
        }

        public IGenericCatalogService<Discipline> Disciplines
        {
            get => disciplinesService = disciplinesService ?? new GenericCatalogService<Discipline>(context);
        }

        public IGenericCatalogService<GroupStudies> GroupStudies
        {
            get => groupStudiesService = groupStudiesService ?? new GenericCatalogService<GroupStudies>(context);
        }

        public IGenericCatalogService<IndividualStudies> IndividualStudies
        {
            get => individualStudiesService = individualStudiesService ?? new GenericCatalogService<IndividualStudies>(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

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
