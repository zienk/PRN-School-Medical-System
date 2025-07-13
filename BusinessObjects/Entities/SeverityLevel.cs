using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;
public partial class SeverityLevel
{
    public int SeverityId { get; set; }

    public string? SeverityName { get; set; }

    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}
