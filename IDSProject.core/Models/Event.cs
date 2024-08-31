using System;
using System.Collections.Generic;

namespace DemoAPI.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Destination { get; set; }

    public DateOnly DateFrom { get; set; }

    public DateOnly DateTo { get; set; }

    public int Cost { get; set; }

    public string? Status { get; set; }

    public int CategoryCode { get; set; }

    public virtual LookUp CategoryCodeNavigation { get; set; } = null!;

    public virtual ICollection<EventGuide> EventGuides { get; set; } = new List<EventGuide>();

    public virtual ICollection<EventMember> EventMembers { get; set; } = new List<EventMember>();
}
