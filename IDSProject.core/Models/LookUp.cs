using System;
using System.Collections.Generic;

namespace IDSProject.Models;

public partial class LookUp
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public int Order { get; set; }
}
