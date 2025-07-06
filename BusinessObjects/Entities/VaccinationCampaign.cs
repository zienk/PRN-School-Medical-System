using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class VaccinationCampaign
{
    public int CampaignId { get; set; }

    public string? VaccineName { get; set; }

    public DateOnly? Date { get; set; }

    public string? Description { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();
}
