using BusinessObjects.Entities;
using DataAccessLayer;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class CampaignStatusRepository : ICampaignStatusRepository
    {
        private readonly PrnEduHealthContext _context;

        public CampaignStatusRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public List<CampaignStatus> GetAllCampaignStatuses()
        {
            return _context.CampaignStatuses.ToList();
        }
    }
}
