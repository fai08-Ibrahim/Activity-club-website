using System;
using System.Collections.Generic;

namespace IDSProject.Models;

public partial class Guide
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public DateOnly JoiningDate { get; set; }

    public byte[]? Photo { get; set; }

    public string? Profession { get; set; }

    public virtual ICollection<EventGuide> EventGuides { get; set; } = new List<EventGuide>();
}
