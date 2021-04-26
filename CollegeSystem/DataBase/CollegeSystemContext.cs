using CollegeSystem.Models.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeSystem.DataBase
{
    public class CollegeSystemContext : DbContext
    { 
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Grades> Grades { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }

        public CollegeSystemContext() : base("CollegeSystemContext")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<CollegeSystemContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Subjects>()
            .HasRequired<Courses>(s => s.Course)
            .WithMany(g => g.Subjects)
            .HasForeignKey<int>(s => s.IdCourse);

            modelBuilder.Entity<Subjects>()
            .HasRequired<Teacher>(s => s.Teacher)
            .WithMany(g => g.Subjects)
            .HasForeignKey<int>(s => s.IdTeacher);
        }
    }
}
