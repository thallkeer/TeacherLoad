using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class GroupLoadService : GenericCatalogService<GroupLoad>, IGroupLoadService
    {
        public GroupLoadService(TeacherLoadContext context) : base(context)
        {}

        public override IEnumerable<GroupLoad> GetAll()
        {
            return dbSet.Include(i => i.Teacher)
                        .Include(i => i.Discipline)
                        .Include(i => i.Group)
                        .Include(i => i.GroupStudies).ToList();
        }

        public IEnumerable<GroupLoad> GetDisciplinesByGroup(string groupNumber)
        {            
            return dbSet.Where(x => x.GroupNumber == groupNumber).Where(x => x.GroupStudies.GroupClassName == "Лекция")
                 .Include(x => x.Teacher).ToList();
        }
    }
}
