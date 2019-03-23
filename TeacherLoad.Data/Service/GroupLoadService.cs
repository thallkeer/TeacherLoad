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

        public override IEnumerable<GroupLoad> GetAll()
        {
            return dbSet.Include(i => i.Teacher)
                        .Include(i => i.Discipline)
                        .Include(i => i.Group)
                        .Include(i => i.GroupStudies).AsNoTracking();
        }

        public override GroupLoad GetByID(object id)
        {
            return Get(gl => gl.GroupLoadID == (int)id, includeProperties: "Teacher,Discipline,GroupStudies,Group").FirstOrDefault();
                        
        }

        public IEnumerable<GroupLoad> GetDisciplinesByGroup(string groupNumber)
        {            
            return dbSet.Where(x => x.GroupNumber == groupNumber)
                  .Include(x => x.Group)
                  .Include(x => x.GroupStudies)
                  .Include(x => x.Discipline)
                  .Include(x => x.Teacher);
        }        

        public IEnumerable<GroupLoad> GetByTeacher(int teacherID)
        {
            return Get(x => x.TeacherID == teacherID,includeProperties: "Teacher,Discipline,GroupStudies");
        }

        public IEnumerable<GroupLoad> GetGroupedLoadsByFilter(Expression<Func<GroupLoad, bool>> filter)
        {
            return Get(filter, includeProperties: "Teacher,Discipline,GroupStudies")
                  .GroupBy(l => new { l.DisciplineID, l.GroupStudiesID })
                  .Select(g => g.First());
        }
    }
}
