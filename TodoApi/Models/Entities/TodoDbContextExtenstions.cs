using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models.Entities
{
    public static class TodoDbContextExtenstions
    {
        public static IQueryable<Task> GetTasks(this TodoDbContext dbContext)
        {
            var query = dbContext.Tasks.AsQueryable();
            return query;
        }

        public static async Task<Task> GetTaskAsync(this TodoDbContext dbContext, Task entity)
            => await dbContext.Tasks.FirstOrDefaultAsync(item => item.TaskId == entity.TaskId);

        public static async Task<Task> GetTaskByNameAsync(this TodoDbContext dbContext, Task entity)
            => await dbContext.Tasks.FirstOrDefaultAsync(item => item.Name == entity.Name);
    }

    public static class IQueryableExtensions
    {
        public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
            => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
    }
}
