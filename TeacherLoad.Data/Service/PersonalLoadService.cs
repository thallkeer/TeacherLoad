using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Add(PersonalLoad personalLoad)
        {
            dbSet.Add(personalLoad);
        }

        public void Delete(int id)
        {
            Delete(dbSet.Find(id));
        }

        public void Delete(PersonalLoad personalLoad)
        {
            if (context.Entry(personalLoad).State == EntityState.Detached)
            {
                dbSet.Attach(personalLoad);
            }
            dbSet.Remove(personalLoad);
        }

        public List<PersonalLoad> GetAll()
        {
            return dbSet.Include(pl => pl.Teacher).Include(pl => pl.IndividualStudies).ToList();
        }

        public PersonalLoad GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Update(PersonalLoad personalLoad)
        {
            dbSet.Attach(personalLoad);
            context.Entry(personalLoad).State = EntityState.Modified;
        }
    }
}
