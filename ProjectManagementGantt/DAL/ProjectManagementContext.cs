using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.DAL
{
    public class ProjectManagementContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*
            // https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var DbPath = System.IO.Path.Join(path, "blogging.db");
            */

            // TODO Datenbank ist nach Migration im falsche Pfad

            optionsBuilder.UseSqlite("Data Source=PMDatabase.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("project");
            modelBuilder.Entity<Phase>().ToTable("phase");
            modelBuilder.Entity<Employee>().ToTable("employee");

            
            modelBuilder.Entity<Project>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.Projects);
            //.HasForeignKey(e => e.EmployeeId)
            //.OnDelete(DeleteBehavior.Restrict); ;
            //.IsRequired();*/

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithOne(e => e.Employee);


        }
    }

}
