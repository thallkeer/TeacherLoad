using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class PersonalLoadService : IPersonalLoadService
    {
        private TeacherLoadContext context;
        private DbSet<PersonalLoad> dbSet;
        public PersonalLoadService(TeacherLoadContext context)
        {
            this.context = context;
            dbSet = context.PersonalLoads;
        }

        public void Add(IPersonalLoadService entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(IPersonalLoadService entity)
        {
            throw new NotImplementedException();
        }

        public List<IPersonalLoadService> GetAll()
        {
            throw new NotImplementedException();
        }

        public IPersonalLoadService GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TeacherLoadContext GetContext()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(IPersonalLoadService entity)
        {
            throw new NotImplementedException();
        }
    }
}
