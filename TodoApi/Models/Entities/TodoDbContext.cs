using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models.Entities
{
    public class TodoDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TasksConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options) { }
    }
}
