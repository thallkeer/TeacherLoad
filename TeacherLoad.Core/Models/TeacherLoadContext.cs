using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TeacherLoad.Core.Models
{
    public class TeacherLoadContext : IdentityDbContext<User>
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
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Position>()
                .HasIndex(p => new { p.PositionID, p.PositionName }).IsUnique();
            modelBuilder.Entity<Teacher>()
                 .HasIndex(t => new { t.FirstName, t.LastName, t.Patronym }).IsUnique();
            modelBuilder.Entity<Discipline>()
                  .HasAlternateKey(d => new { d.DisciplineName });
            modelBuilder.Entity<GroupStudies>()
                 .HasAlternateKey(gs => new { gs.ClassType });
            modelBuilder.Entity<IndividualStudies>()
                 .HasAlternateKey(ind => new { ind.IndividualClassType });
            modelBuilder.Entity<User>().ToTable("IdentityUser");
        }
    }
}
