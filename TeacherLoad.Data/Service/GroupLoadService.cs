using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            Delete(dbSet.Find(id));
        }

        public void Delete(GroupLoad entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public List<GroupLoad> GetAll()
        {
            return dbSet.Include(i => i.Teacher)
                        .Include(i => i.Discipline)
                        .Include(i => i.Group)
                        .Include(i => i.GroupStudies).ToList();
        }

        public GroupLoad GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Update(GroupLoad entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public List<GroupLoad> GetDisciplinesByGroup(string groupNumber)
        {            
            return dbSet.Where(x => x.GroupNumber == groupNumber).Where(x => x.GroupStudies.GroupClassName == "Лекция")
                 .Include(x => x.Teacher).ToList();
        }
    }
}
