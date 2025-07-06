using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class Student
{
    public int StudentId { get; set; }

    public string? FullName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int GenderId { get; set; }

    public string? Class { get; set; }

    public Guid? ParentId { get; set; }

    public bool? IsActive { get; set; }

    public virtual GenderType Gender { get; set; } = null!;

    public virtual ICollection<HealthCheckupResult> HealthCheckupResults { get; set; } = new List<HealthCheckupResult>();

    public virtual HealthRecord? HealthRecord { get; set; }

    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();

    public virtual User? Parent { get; set; }

    public virtual ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();
}
