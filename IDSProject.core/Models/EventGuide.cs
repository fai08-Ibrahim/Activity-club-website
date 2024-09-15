using System;
using System.Collections.Generic;

namespace IDSProject.Models;

public partial class EventGuide
{
    public int Id { get; set; }

    public int GuideId { get; set; }

    public int EventId { get; set; }
    public string EventName { get; set; } = null!;
    public virtual Event? Event { get; set; }

    public virtual Guide? Guide { get; set; }
}
