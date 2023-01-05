using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using schoolproject.Models;

namespace schoolproject.Data
{
    public partial class SchoolDbContext : DbContext
    {
        public SchoolDbContext()
        {
        }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeCourse> EmployeeCourses { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<SupportClass> SupportClasses { get; set; } = null!;
        public virtual DbSet<Title> Titles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-EQE3P3DB; Initial Catalog=School; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.ClassName).HasMaxLength(50);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseInfo).HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.FkTitleId).HasColumnName("FK_TitleId");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PersonalNumber).HasMaxLength(15);

                entity.Property(e => e.YearOfEmployment).HasColumnType("date");
            });

            modelBuilder.Entity<EmployeeCourse>(entity =>
            {
                entity.ToTable("Employee_Course");

                entity.Property(e => e.EmployeeCourseId).HasColumnName("Employee_CourseId");

                entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseId");

                entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeId");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.EmployeeCourses)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("FK_Employee_Course_Course");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.EmployeeCourses)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .HasConstraintName("FK_Employee_Course_Employee");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseId");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

                entity.Property(e => e.SetDate).HasColumnType("date");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("FK_Grade_Course");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.FkStudentId)
                    .HasConstraintName("FK_Grade_Student");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.FkClassId).HasColumnName("FK_ClassId");

                entity.Property(e => e.FkSupportClassId).HasColumnName("FK_SupportClassId");

                entity.Property(e => e.LastName).HasMaxLength(35);

                entity.Property(e => e.PersonalNumber).HasMaxLength(15);

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.FkClassId)
                    .HasConstraintName("FK_Student_Class");

                entity.HasOne(d => d.FkSupportClass)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.FkSupportClassId)
                    .HasConstraintName("FK_Student_SupportClass");
            });

            modelBuilder.Entity<SupportClass>(entity =>
            {
                entity.ToTable("SupportClass");

                entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeId");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.SupportClasses)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .HasConstraintName("FK_SupportClass_Employee");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.SupportClasses)
                    .HasForeignKey(d => d.FkStudentId)
                    .HasConstraintName("FK_SupportClass_Student");
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.ToTable("Title");

                entity.Property(e => e.Title1)
                    .HasMaxLength(50)
                    .HasColumnName("Title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
