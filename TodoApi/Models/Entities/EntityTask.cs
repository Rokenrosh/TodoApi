using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models.Entities
{
    public partial class Task
    {
        public Task()
        {

        }

        public Task(int? taskId)
        {
            TaskId = taskId;
        }

        public int? TaskId { get; set; }

        public string Name { get; set; }

        public Task Parent { get; set; }

        public int? ParentId { get; set; }

        public int? UserId { get; set; }

        public ICollection<Task> Subtasks { get; set; }

        public int? Priority { get; set; }

        public int? Status { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double Period { get; set; }

        public string Project { get; set; }

        public List<Employee> Members { get; set; }

        public List<Employee> Admins { get; set; }

        public DateTime? CreationDate { get; set; }

        public string Description { get; set; }

        public DateTime? LastChangeDate { get; set; }
    }

    public class TasksConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Tasks", "ToDo");
            builder.HasKey(p => p.TaskId);
            builder.Property(p => p.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.ParentId).HasColumnType("int");
            builder.Property(p => p.UserId).HasColumnType("int");
            builder.HasMany(p => p.Subtasks).WithOne().HasForeignKey("ParentId").IsRequired(false);
            builder.Property(p => p.Priority).HasColumnType("int");
            builder.Property(p => p.Status).HasColumnType("int");
            builder.Property(p => p.StartTime).HasColumnType("datetime2");
            builder.Property(p => p.EndTime).HasColumnType("datetime2");
            builder.Property(p => p.Period).HasColumnType("float");
            builder.Property(p => p.Project).HasColumnType("nvarchar(50)");
            builder.HasMany(p => p.Members).WithOne().HasForeignKey("UserId").IsRequired();
            builder.HasMany(p => p.Admins).WithOne().HasForeignKey("UserId").IsRequired();
            builder.Property(p => p.Description).HasColumnType("nvarchar(1000)");

            builder.Property(p => p.TaskId)
                .HasColumnType("int")
                .IsRequired()
                .HasDefaultValueSql("NEXT VALUE FOR [Sequences].[TaskId]");

            builder.Property(p => p.CreationDate).HasColumnType("datetime2").IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.LastChangeDate).HasColumnType("datetime2").IsRequired().ValueGeneratedOnAddOrUpdate();
        }
    }

}

