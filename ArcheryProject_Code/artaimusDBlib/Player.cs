using System;
using System.Collections.Generic;

namespace ArcheryProject;

public partial class Player
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Nickname { get; set; }

    public ulong Admin { get; set; }

    public int? LoginsId { get; set; }

    public virtual ICollection<EventsHasPlayer> EventsHasPlayers { get; set; } = new List<EventsHasPlayer>();

    public virtual Login? Logins { get; set; }
}
