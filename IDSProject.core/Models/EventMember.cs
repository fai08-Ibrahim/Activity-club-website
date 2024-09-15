using System;
using System.Collections.Generic;

namespace IDSProject.Models;

public partial class EventMember
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int EventId { get; set; }
    public string EventName { get; set; } = null!;

    public virtual Event? Event { get; set; }

    public virtual Member? Member { get; set; }
}
