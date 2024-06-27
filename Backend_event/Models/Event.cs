using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend_event.Models;

public partial class Event
{
    public int Id { get; set; }

    public string? Eventname { get; set; }

    public string? Description { get; set; }

    public DateOnly EventDate { get; set; }

    public TimeOnly EventTime { get; set; }

    public string? Type { get; set; }

    public string? Username { get; set; }

    public int? Userid { get; set; }

    public DateTime? Timestamp { get; set; }

    [JsonIgnore]
    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
