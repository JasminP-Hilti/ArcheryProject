using System;
using System.Collections.Generic;

namespace artaimusDBlib;

public partial class Parcour
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int CountAnimals { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
