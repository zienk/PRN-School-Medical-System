using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class MedicalEventType
{
    public int EventTypeId { get; set; }

    public string? EventTypeName { get; set; }

    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}
