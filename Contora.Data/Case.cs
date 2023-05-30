using System;
using System.Collections.Generic;

namespace Contora.Data;

public partial class Case
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public int PrimaryAgentId { get; set; }

    public int? SecondaryAgentId { get; set; }

    public DateOnly DateOpen { get; set; }

    public DateOnly? DateClose { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Agent PrimaryAgent { get; set; } = null!;

    public virtual Agent? SecondaryAgent { get; set; }

    public virtual ICollection<Target> Targets { get; set; } = new List<Target>();
}
