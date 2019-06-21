using TodoApi.Models.Entities;
using Task = System.Threading.Tasks.Task;

namespace TodoApi.Services
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(TodoDbContext todoDbContext)
        {
            todoDbContext.Roles.Add(new Role { Name = "Admin" });
            todoDbContext.Roles.Add(new Role { Name = "User" });
            await todoDbContext.SaveChangesAsync();
        }
    }
}
