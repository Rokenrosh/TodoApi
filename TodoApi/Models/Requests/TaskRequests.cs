using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models.Requests
{
    public class PostTaskRequest
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public Entities.Task Parent { get; set; }

        public int? ParentId { get; set; }

        [Required]
        public int UserId { get; set; }

        public ICollection<Entities.Task> Subtasks { get; set; }

        public int? Priority { get; set; }

        [Required]
        public int Status { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double Period { get; set; }

        [StringLength(50)]
        public string Project { get; set; }

        [Required]
        public List<Employee> Members { get; set; }

        [Required]
        public List<Employee> Admins { get; set; }

        [Required]
        public DateTime? CreationDate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public DateTime? LastChangeDate { get; set; }
    }

    public class PutTaskRequest
    {
        public string Name { get; set; }

        public int? Priority { get; set; }

        public int Status { get; set; }

        public List<Employee> Members { get; set; }

        public List<Employee> Admins { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime? LastChangeDate { get; set; }
    }

    public static class Extensions
    {
        public static Entities.Task ToEntity(this PostTaskRequest request)
            => new Entities.Task
            {
                TaskId = request.TaskId,
                Parent = request.Parent,
                ParentId = request.ParentId,
                Description = request.Description,
                CreationDate = request.CreationDate,
                LastChangeDate = request.LastChangeDate,
                Members = request.Members,
                Admins = request.Admins,
                Priority = request.Priority,
                Project = request.Project,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Period = request.Period,
                UserId = request.UserId,
                Subtasks = request.Subtasks,
                Name = request.Name,
                Status = request.Status
            };
    }
}
