using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDSProject.core.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Name { get; set; }

        public string? Role { get; set; }
    }
}
