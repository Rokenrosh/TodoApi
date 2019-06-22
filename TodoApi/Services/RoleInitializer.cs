using Microsoft.EntityFrameworkCore;
using System.Linq;
using TodoApi.Models.Entities;
using Task = System.Threading.Tasks.Task;

namespace TodoApi.Services
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(TodoDbContext todoDbContext)
        {
            if (await todoDbContext.Roles.SingleOrDefaultAsync(x => x.Name == "Admin") == null)
                todoDbContext.Roles.Add(new Role { Name = "Admin" });
            if (await todoDbContext.Roles.SingleOrDefaultAsync(x => x.Name == "User") == null)
                    todoDbContext.Roles.Add(new Role { Name = "User" });
            await todoDbContext.SaveChangesAsync();
        }
    }
}
