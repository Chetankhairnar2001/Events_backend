using System;
using System.Collections.Generic;

namespace Backend_event.Models;

public partial class Favorite
{
    public int Id { get; set; }

    public string? Currentusername { get; set; }

    public int? Currentuserid { get; set; }

    public int? Eventid { get; set; }

    public virtual Event? Event { get; set; }
}
