using System;
using System.Collections.Generic;

namespace Contora.Data;

public partial class Target
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public int CaseId { get; set; }

    public string? Phone { get; set; }

    public DateOnly? Birthdate { get; set; }

    public string? Address { get; set; }

    public string? AdditionalInfo { get; set; }

    public virtual Case Case { get; set; } = null!;
}
