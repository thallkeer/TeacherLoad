﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Core.Migrations
{
    [DbContext(typeof(TeacherLoadContext))]
    [Migration("20190324180111_AddRestrict")]
    partial class AddRestrict
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("IdentityUser");
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartmentName")
                        .IsRequired();

                    b.HasKey("DepartmentID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.Discipline", b =>
                {
                    b.Property<int>("DisciplineID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisciplineName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("DisciplineID");

                    b.HasIndex("DisciplineName")
                        .IsUnique();

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.Group", b =>
                {
                    b.Property<string>("GroupNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SpecialityCode")
                        .IsRequired();

                    b.Property<int>("StudentsCount");

                    b.HasKey("GroupNumber");

                    b.HasIndex("SpecialityCode");

                    b.ToTable("StudyGroup");
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.GroupLoad", b =>
                {
                    b.Property<int>("GroupLoadID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisciplineID");

                    b.Property<string>("GroupNumber")
                        .IsRequired();

                    b.Property<int>("GroupStudiesID");

                    b.Property<int>("Semester");

                    b.Property<int>("StudyType");

                    b.Property<int>("StudyYear");

                    b.Property<int>("TeacherID");

                    b.Property<int>("VolumeHours")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.HasKey("GroupLoadID");

                    b.HasIndex("DisciplineID");

                    b.HasIndex("GroupNumber");

                    b.HasIndex("GroupStudiesID");

                    b.HasIndex("TeacherID", "GroupStudiesID", "GroupNumber", "DisciplineID", "Semester", "StudyType", "StudyYear")
                        .IsUnique();

                    b.ToTable("GroupLoads");
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.GroupStudies", b =>
                {
                    b.Property<int>("GroupClassID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupClassName")
                        .IsRequired();

                    b.HasKey("GroupClassID");

                    b.HasIndex("GroupClassName")
                        .IsUnique();

                    b.ToTable("GroupStudies");
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.IndividualStudies", b =>
                {
                    b.Property<int>("IndividualClassID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IndividualClassName")
                        .IsRequired();

                    b.Property<int>("VolumeByPerson")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.HasKey("IndividualClassID");

                    b.HasIndex("IndividualClassName")
                        .IsUnique();

                    b.ToTable("PersonalStudies");
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.PersonalLoad", b =>
                {
                    b.Property<int>("PersonalLoadID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IndividualClassID");

                    b.Property<int>("StudentsCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<int>("TeacherID");

                    b.HasKey("PersonalLoadID");

                    b.HasIndex("IndividualClassID");

                    b.HasIndex("TeacherID", "IndividualClassID")
                        .IsUnique();

                    b.ToTable("PersonalLoads");
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.Position", b =>
                {
                    b.Property<int>("PositionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PositionName")
                        .IsRequired();

                    b.HasKey("PositionID");

                    b.HasIndex("PositionName")
                        .IsUnique();

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.Speciality", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EducationLevel");

                    b.Property<string>("SpecialityName")
                        .IsRequired();

                    b.HasKey("Code");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentID");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Patronym")
                        .IsRequired();

                    b.Property<int>("PositionID");

                    b.HasKey("TeacherID");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("PositionID");

                    b.HasIndex("FirstName", "LastName", "Patronym")
                        .IsUnique();

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TeacherLoad.Core.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TeacherLoad.Core.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeacherLoad.Core.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TeacherLoad.Core.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.Group", b =>
                {
                    b.HasOne("TeacherLoad.Core.Models.Speciality", "Speciality")
                        .WithMany("Groups")
                        .HasForeignKey("SpecialityCode")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.GroupLoad", b =>
                {
                    b.HasOne("TeacherLoad.Core.Models.Discipline", "Discipline")
                        .WithMany("GroupLoads")
                        .HasForeignKey("DisciplineID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TeacherLoad.Core.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupNumber")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeacherLoad.Core.Models.GroupStudies", "GroupStudies")
                        .WithMany()
                        .HasForeignKey("GroupStudiesID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeacherLoad.Core.Models.Teacher", "Teacher")
                        .WithMany("GroupLoads")
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.PersonalLoad", b =>
                {
                    b.HasOne("TeacherLoad.Core.Models.IndividualStudies", "IndividualStudies")
                        .WithMany()
                        .HasForeignKey("IndividualClassID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeacherLoad.Core.Models.Teacher", "Teacher")
                        .WithMany("PersonalLoads")
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeacherLoad.Core.Models.Teacher", b =>
                {
                    b.HasOne("TeacherLoad.Core.Models.Department", "Department")
                        .WithMany("Teachers")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeacherLoad.Core.Models.Position", "Position")
                        .WithMany("Teachers")
                        .HasForeignKey("PositionID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
