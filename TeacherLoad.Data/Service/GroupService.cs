using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class GroupService : IGroupService
    {
        private TeacherLoadContext context;
        private DbSet<Group> groups;

        public GroupService(TeacherLoadContext context)
        {
            this.context = context;
            groups = context.Groups;
        }

        public void Add(Group group)
        {
            groups.Add(group);
        }

        public void Delete(int id)
        {
            Delete(groups.Find(id));
        }

        public void Delete(Group group)
        {
            if (context.Entry(group).State == EntityState.Detached)
            {
                groups.Attach(group);
            }
            groups.Remove(group);
        }

        public List<Group> GetAll()
        {
            return groups.Include(g => g.Speciality).ToList();
        }

        public Group GetById(string id)
        {           
            return groups.Find(id);
        }

        public Group GetById(int id)
        {
            throw new NotImplementedException();
        }       

        public void Update(Group group)
        {
            groups.Attach(group);
            context.Entry(group).State = EntityState.Modified;
        }
    }
}
