using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace TodoApi.Models.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<TaskAdmins> TaskAdmins { get; set; }
        public ICollection<TaskMembers> TaskMembers { get; set; }
        public Employee()
        {
            Tasks = new List<Task>();
            TaskAdmins = new List<TaskAdmins>();
            TaskMembers = new List<TaskMembers>();
        }
    }

    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(p => p.EmployeeId);
            builder.HasAlternateKey(p => p.Email);

            builder.Property(p => p.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.Surname).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.Position).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.RoleId).HasColumnType("int").IsRequired();
            builder.Property(p => p.Email).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(p => p.Password).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.Nickname).HasColumnType("nvarchar(30)").IsRequired();

            builder.HasMany(p => p.TaskAdmins)
                .WithOne()
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.TaskMembers)
                .WithOne()
                .HasForeignKey(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(p => p.Role)
                .WithMany()
                .HasForeignKey(p => p.RoleId);
            builder
                .Property(p => p.EmployeeId)
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder
                .HasMany(x => x.Tasks)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public Role()
        {

        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(p => p.RoleId);

            builder.Property(p => p.Name).HasColumnType("nvarchar(20)").IsRequired();

            builder
                .Property(p => p.RoleId)
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();
        }
    }
}
