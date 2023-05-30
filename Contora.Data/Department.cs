using System;
using System.Collections.Generic;

namespace Contora.Data;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
}
