using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherLoad.Core.Models
{
    public class TeacherLoadContext : DbContext
    {
        public TeacherLoadContext(DbContextOptions<TeacherLoadContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupLoad> GroupLoads { get; set; }
        public DbSet<GroupStudies> GroupStudies { get; set; }
        public DbSet<IndividualStudies> PersonalStudies { get; set; }
        public DbSet<PersonalLoad> PersonalLoads { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

    }
}
