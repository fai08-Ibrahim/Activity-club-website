using System;
using System.Collections.Generic;

namespace DemoAPI.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Name { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Role { get; set; }

    public int Id { get; set; }
}
