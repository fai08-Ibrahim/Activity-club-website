using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDSProject.Models;

public partial class User
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Name { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Role { get; set; }

}
