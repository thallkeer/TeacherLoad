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
    public class TeacherService : ITeacherService
    {
        DbSet<Teacher> teachers;
        TeacherLoadContext context;
        public TeacherService(TeacherLoadContext context)
        {
            this.context = context;
            teachers = context.Teachers;
        }

        public void Add(Teacher teacher)
        {
            teachers.Add(teacher);
        }

        public void Delete(int id)
        {
            Delete(teachers.Find(id));
        }

        public void Delete(Teacher teacher)
        {
            if (context.Entry(teacher).State == EntityState.Detached)
            {
                teachers.Attach(teacher);
            }
            teachers.Remove(teacher);
        }

        public List<Teacher> GetAll()
        {
            return teachers.Include(t => t.Department).Include(t => t.Position).ToList();
        }

        public Teacher GetById(int id)
        {
            Teacher teacher = teachers.Where(t => t.TeacherID == id)
                .Include(t => t.Department)
                .Include(t => t.Position).FirstOrDefault();

            return teacher;
        }

        public void Update(Teacher teacher)
        {
            teachers.Attach(teacher);
            context.Entry(teacher).State = EntityState.Modified;
        }
    }
}
