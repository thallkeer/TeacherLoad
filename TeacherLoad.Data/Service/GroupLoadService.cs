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
                        .Include(i => i.GroupStudies).AsNoTracking().ToList();
        }

        public IEnumerable<GroupLoad> GetDisciplinesByGroup(string groupNumber)
        {            
            return dbSet.Where(x => x.GroupNumber == groupNumber)
                  .Include(x => x.Group)
                  .Include(x => x.GroupStudies)
                  .Include(x => x.Discipline)
                  .Include(x => x.Teacher).AsNoTracking().ToList();
        }

        /// <summary>
        /// Для упрощения работы со сложным составным ключом 
        /// </summary>
        /// <param name="load"></param>
        /// <returns></returns>
        public override GroupLoad GetByID(object load)
        {
            GroupLoad groupLoad = load as GroupLoad;
            return Get(gl => gl.TeacherID == groupLoad.TeacherID && gl.GroupStudiesID == groupLoad.GroupStudiesID
                   && gl.GroupNumber == groupLoad.GroupNumber && gl.Semester == groupLoad.Semester
                   && gl.StudyType == groupLoad.StudyType && gl.StudyYear == groupLoad.StudyYear
                   && gl.DisciplineID == groupLoad.DisciplineID,includeProperties: "GroupStudies,Discipline,Teacher,Group").FirstOrDefault();
        }

        public IEnumerable<GroupLoad> GetByTeacher(int teacherID)
        {
            return Get(x => x.TeacherID == teacherID,includeProperties: "Teacher,Discipline,GroupStudies");
        }
    }
}
