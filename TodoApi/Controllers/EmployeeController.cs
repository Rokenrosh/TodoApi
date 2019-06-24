using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Entities;

namespace TodoApi.Controllers
{
    [Route("api/v1/user[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly TodoDbContext _todoDbContext;
        public EmployeeController(TodoDbContext todoDbContext)
        {
            this._todoDbContext = todoDbContext;
        }

        [HttpGet("{id}")]
        public async Task<Employee> GetEmployee(int id) => await _todoDbContext.Employees.SingleOrDefaultAsync(x => x.EmployeeId == id);

        [HttpPost]
        public async Task<IActionResult> AddEmployee()
        {
            var role = await _todoDbContext.Roles.SingleOrDefaultAsync(x => x.Name == "Admin");
            var emp = new Employee {
                Email = "spritefok@gmail.com",
                Name = "Филипп",
                Surname = "Хамицевич",
                Position = "Программист",
                Nickname = "homa_inc",
                Password = "123456",
                RoleId = role.RoleId,
                Role = role
            };
            _todoDbContext.Add(emp);
            await _todoDbContext.SaveChangesAsync();
            return new ObjectResult(emp);
        }
    }
}