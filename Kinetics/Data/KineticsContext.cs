using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kinetics.Models;

namespace Kinetics.Data
{
    public class KineticsContext : DbContext
    {
        public KineticsContext (DbContextOptions<KineticsContext> options)
            : base(options)
        {
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<FinancialAssistanceRequest> FinancialAssistanceRequests { get; set; }
        public DbSet<LeaveApplication> LeaveApplicationRequests { get; set; }
        public DbSet<EmployeeTraining> EmployeeTrainings { get; set; }
        public DbSet<EmployeeProperty> EmployeeProperties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Define relationships and addititional configurations

            modelBuilder.Entity<Employee>()
            .HasOne(e => e.LineManager)
            .WithMany(m => m.Subordinates)
            .HasForeignKey(e => e.LineManagerID);

            modelBuilder.Entity<Salary>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.Salaries)
                .HasForeignKey(s => s.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentID);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Role)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.RoleID);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.JobPosition)
                .WithMany(j => j.Employees)
                .HasForeignKey(e => e.PositionID);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Branch)
                .WithMany(b => b.Employees)
                .HasForeignKey(e => e.BranchID);
        }
    }
}
