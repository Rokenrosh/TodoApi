using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models.Entities
{
    public class Employee
    {
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

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role()
        {

        }
    }
}
