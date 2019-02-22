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

        public UnitOfWork(TeacherLoadContext context)
        {
            this.context = context;
        }

        public ITeacherService TeacherService
        {
            get => teacherService = teacherService ?? new TeacherService(context);            
        }

        public IGroupService GroupService
        {
            get => groupService = groupService ?? new GroupService(context);
        }

        public IGroupLoadService GroupLoadService
        {
            get => groupLoadService = groupLoadService ?? new GroupLoadService(context);
        }

        public IPersonalLoadService PersonalLoadService
        {
            get => personalLoadService = personalLoadService ?? new PersonalLoadService(context);
        }

        public IGenericCatalogService<Position> PositionsService
        {
            get => positionsService = positionsService ?? new GenericCatalogService<Position>(context);
        }

        public IGenericCatalogService<Department> DepartmentsService
        {
            get => departmentsService = departmentsService ?? new GenericCatalogService<Department>(context);
        }

        public IGenericCatalogService<Speciality> SpecialitiesService
        {
            get => specialitiesService = specialitiesService ?? new GenericCatalogService<Speciality>(context);
        }

        public IGenericCatalogService<Discipline> DisciplinesService
        {
            get => disciplinesService = disciplinesService ?? new GenericCatalogService<Discipline>(context);
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
