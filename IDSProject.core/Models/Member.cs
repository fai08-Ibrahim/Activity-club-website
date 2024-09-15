using System;
using System.Collections.Generic;

namespace IDSProject.Models;

public partial class Member
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly JoiningDate { get; set; }

    public string MobileNumber { get; set; } = null!;

    public string? EmergencyNum { get; set; }

    public byte[]? Photo { get; set; }

    public string? Profession { get; set; }

    public string? Nationality { get; set; }

    public virtual ICollection<EventMember> EventMembers { get; set; } = new List<EventMember>();
}
