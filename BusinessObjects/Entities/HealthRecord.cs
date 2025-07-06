using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class HealthRecord
{
    public int HealthRecordId { get; set; }

    public int? StudentId { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public string? ChronicDiseases { get; set; }

    public string? Allergies { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastUpdatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual Student? Student { get; set; }
}
