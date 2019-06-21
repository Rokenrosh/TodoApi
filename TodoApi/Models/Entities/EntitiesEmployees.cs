using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public Employee()
        {

        }
    }

    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(p => p.EmployeeId);

            builder.Property(p => p.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.Surname).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.Position).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.RoleId).HasColumnType("int").IsRequired();
            builder.Property(p => p.Email).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(p => p.Password).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.Nickname).HasColumnType("nvarchar(30)").IsRequired();
        }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role()
        {

        }
    }
}
