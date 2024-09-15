using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDSProject.core.Dtos
{
    public class MemberDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;

        public string Gender { get; set; } = null!;
        public DateOnly JoiningDate { get; set; }
        public string? Nationality { get; set; }
    }
}
