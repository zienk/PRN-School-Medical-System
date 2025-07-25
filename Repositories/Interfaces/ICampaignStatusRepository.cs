using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ICampaignStatusRepository
    {
        // Lấy danh sách campaign statuses để show trong dropdown, combobox
        List<CampaignStatus> GetAllCampaignStatuses();
    }
}
