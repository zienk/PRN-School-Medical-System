using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Incident
{
    public int IncidentId { get; set; }

    public int StudentId { get; set; }

    public int IncidentTypeId { get; set; }

    public DateTime IncidentDate { get; set; }

    public string? Description { get; set; }

    public string? ActionsTaken { get; set; }

    public Guid? ReportedBy { get; set; }

    public int? SeverityId { get; set; }

    public string? Location { get; set; }

    public bool? IsActive { get; set; }

    public virtual MedicalEventType IncidentType { get; set; } = null!;

    public virtual User? ReportedByNavigation { get; set; }

    public virtual SeverityLevel? Severity { get; set; }

    public virtual Student Student { get; set; } = null!;
}
