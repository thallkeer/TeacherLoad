using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
        
        public void Reload(GroupLoad groupLoad)
        {
            context.Entry(groupLoad).Reload();
        }
    }
}
