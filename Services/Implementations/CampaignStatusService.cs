using BusinessObjects.Entities;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class CampaignStatusService : ICampaignStatusService
    {
        private readonly ICampaignStatusRepository _campaignStatusRepository;

        public CampaignStatusService()
        {
            _campaignStatusRepository = new CampaignStatusRepository();
        }

        public List<CampaignStatus> GetAllCampaignStatuses()
        {
            return _campaignStatusRepository.GetAllCampaignStatuses();
        }
    }
}
