using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class GroupLoadService : IGroupLoadService
    {
        private TeacherLoadContext context;
        private DbSet<GroupLoad> dbSet;

        public GroupLoadService(TeacherLoadContext context)
        {
            this.context = context;
            dbSet = context.GroupLoads;
        }

        public void Add(GroupLoad entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(GroupLoad entity)
        {
            throw new NotImplementedException();
        }

        public List<GroupLoad> GetAll()
        {
            throw new NotImplementedException();
        }

        public GroupLoad GetById(int id)
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

        public void Update(GroupLoad entity)
        {
            throw new NotImplementedException();
        }
    }
}
