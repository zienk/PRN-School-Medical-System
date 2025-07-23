using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class CampaignStatus
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<HealthCheckup> HealthCheckups { get; set; } = new List<HealthCheckup>();

    public virtual ICollection<VaccinationCampaign> VaccinationCampaigns { get; set; } = new List<VaccinationCampaign>();
}
