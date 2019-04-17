using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class GroupLoadService : GenericService<GroupLoad>, IGroupLoadService
    {
        public GroupLoadService(TeacherLoadContext context) : base(context)
        {}
                
        public IEnumerable<GroupLoad> GetDisciplinesByGroup(string groupNumber)
        {
            return dbSet.Where(x => x.GroupNumber == groupNumber);
        }        

        public IEnumerable<GroupLoad> GetByTeacher(int teacherID)
        {
            return Get(x => x.TeacherID == teacherID);
        }

        public IEnumerable<GroupLoad> GetGroupedLoadsByFilter(Expression<Func<GroupLoad, bool>> filter)
        {
            return Get(filter)
                  .GroupBy(l => new { l.DisciplineID, l.GroupStudiesID })
                  .Select(g => g.First());
        }
    }
}
