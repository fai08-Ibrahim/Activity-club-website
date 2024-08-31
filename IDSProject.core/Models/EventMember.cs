using System;
using System.Collections.Generic;

namespace DemoAPI.Models;

public partial class EventMember
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
