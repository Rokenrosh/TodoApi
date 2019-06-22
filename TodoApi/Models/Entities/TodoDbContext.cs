using Microsoft.EntityFrameworkCore;

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
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new TaskAdminsConfiguration());
            modelBuilder.ApplyConfiguration(new TaskMembersConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
