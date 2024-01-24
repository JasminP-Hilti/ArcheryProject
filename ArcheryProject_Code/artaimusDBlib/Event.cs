using System;
using System.Collections.Generic;

namespace artaimusDBlib;

public partial class Event
{
    public int Id { get; set; }

    public int ParcoursId { get; set; }

    public int CountType { get; set; }

    public virtual ICollection<EventsHasPlayer> EventsHasPlayers { get; set; } = new List<EventsHasPlayer>();

    public virtual Parcour Parcours { get; set; } = null!;
}
