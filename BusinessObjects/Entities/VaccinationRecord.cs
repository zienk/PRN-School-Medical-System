using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class VaccinationRecord
{
    public int VaccinationRecordId { get; set; }

    public int? StudentId { get; set; }

    public int? CampaignId { get; set; }

    public DateTime? VaccinationDate { get; set; }

    public string? Result { get; set; }

    public string? Notes { get; set; }

    public bool? IsActive { get; set; }

    public virtual VaccinationCampaign? Campaign { get; set; }

    public virtual Student? Student { get; set; }
}
