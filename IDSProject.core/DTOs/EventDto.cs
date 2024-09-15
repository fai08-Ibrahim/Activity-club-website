using IDSProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDSProject.core.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        public string? Status { get; set; }
        public int CategoryCode { get; set; }
        public virtual LookUp? CategoryCodeNavigation { get; set; }
    }
}
