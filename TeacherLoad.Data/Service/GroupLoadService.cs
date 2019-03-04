using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            return dbSet.Where(x => x.GroupNumber == groupNumber)
                  .Include(x => x.GroupStudies)
                  .Include(x => x.Discipline)
                  .Include(x => x.Teacher).ToList();
        }

        /// <summary>
        /// Для упрощения работы со сложным составным ключом 
        /// </summary>
        /// <param name="load"></param>
        /// <returns></returns>
        public override GroupLoad GetByID(object load)
        {
            GroupLoad groupLoad = load as GroupLoad;
            
            return dbSet.Find(groupLoad.TeacherID, groupLoad.DisciplineID, groupLoad.GroupNumber,
                groupLoad.GroupStudiesID, groupLoad.Semester, groupLoad.StudyType, groupLoad.StudyYear);
        }


    }
}
