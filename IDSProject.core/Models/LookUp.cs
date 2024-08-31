using System;
using System.Collections.Generic;

namespace DemoAPI.Models;

public partial class LookUp
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public int Order { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
