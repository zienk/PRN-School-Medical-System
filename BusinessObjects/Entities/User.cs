using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public int RoleId { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public bool? IsFirstLogin { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<HealthCheckup> HealthCheckups { get; set; } = new List<HealthCheckup>();

    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<VaccinationCampaign> VaccinationCampaigns { get; set; } = new List<VaccinationCampaign>();
}
