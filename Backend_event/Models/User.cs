using System;
using System.Collections.Generic;

namespace Backend_event.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public bool? Admin { get; set; }
}
