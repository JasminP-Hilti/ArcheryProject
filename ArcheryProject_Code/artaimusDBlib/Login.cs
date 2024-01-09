using System;
using System.Collections.Generic;

namespace ArcheryProject;

public partial class Login
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
