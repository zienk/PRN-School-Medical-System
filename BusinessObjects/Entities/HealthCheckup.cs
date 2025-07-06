using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class HealthCheckup
{
    public int CheckupId { get; set; }

    public string? CheckupName { get; set; }

    public DateOnly? CheckupDate { get; set; }

    public string? Description { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<HealthCheckupResult> HealthCheckupResults { get; set; } = new List<HealthCheckupResult>();
}
