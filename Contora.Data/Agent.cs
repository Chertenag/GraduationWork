using System;
using System.Collections.Generic;

namespace Contora.Data;

public partial class Agent
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public int DepartmentId { get; set; }

    public int PositionId { get; set; }

    public int RankId { get; set; }

    public int StatusId { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Case> CasePrimaryAgents { get; set; } = new List<Case>();

    public virtual ICollection<Case> CaseSecondaryAgents { get; set; } = new List<Case>();

    public virtual Department Department { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;

    public virtual Rank Rank { get; set; } = null!;

    public virtual Agentstatus Status { get; set; } = null!;
}
