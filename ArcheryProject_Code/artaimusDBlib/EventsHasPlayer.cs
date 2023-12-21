using System;
using System.Collections.Generic;

namespace ArcheryProject;

public partial class EventsHasPlayer
{
    public int EventsId { get; set; }

    public int PlayersId { get; set; }

    public int PointsTotal { get; set; }

    public virtual Event Events { get; set; } = null!;

    public virtual Player Players { get; set; } = null!;
}
