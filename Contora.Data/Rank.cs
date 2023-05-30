using System;
using System.Collections.Generic;

namespace Contora.Data;

public partial class Rank
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
}
