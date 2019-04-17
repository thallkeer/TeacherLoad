using System.Collections.Generic;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TeacherLoad.Data.Service
{
    public class TeacherService : GenericService<Teacher>,ITeacherService
    {
        public TeacherService(TeacherLoadContext context) : base(context)
        {}
        
        public IEnumerable<Teacher> GetAllForSelectList()
        {
            return dbSet.AsNoTracking();
        }        
    }
}
