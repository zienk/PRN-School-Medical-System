using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class HealthCheckupResult
{
    public int ResultId { get; set; }

    public int StudentId { get; set; }

    public int CheckupId { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Bmi { get; set; }

    public string? Vision { get; set; }

    public string? DentalStatus { get; set; }

    public string? BloodPressure { get; set; }

    public int? HeartRate { get; set; }

    public string? GeneralCondition { get; set; }

    public string? Notes { get; set; }

    public DateTime? CheckupTime { get; set; }

    public bool? IsActive { get; set; }

    public virtual HealthCheckup Checkup { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
