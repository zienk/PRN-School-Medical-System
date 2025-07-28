using BusinessObjects.Entities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class VaccinationCampaignRepository : IVaccinationCampaignRepository 
    {
        private readonly PrnEduHealthContext _context;

        public VaccinationCampaignRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public VaccinationCampaign AddVaccinationCampaign(VaccinationCampaign campaign)
        {
            _context.VaccinationCampaigns.Add(campaign);
            _context.SaveChanges();
            return campaign;
        }

        public bool DeleteVaccinationCampaign(int campaignId)
        {
            return _context.VaccinationCampaigns
                .Where(c => c.CampaignId == campaignId)
                .ExecuteUpdate(c => c.SetProperty(c => c.IsActive, false)) > 0;
        }

        public List<VaccinationCampaign> GetAllVaccinationCampaigns()
        {
            return _context.VaccinationCampaigns
                .Include(c => c.Status)
                .Where(c => c.IsActive == true)
                .ToList();
        }

        public VaccinationCampaign? GetVaccinationCampaignById(int campaignId)
        {
            return _context.VaccinationCampaigns
                .FirstOrDefault(c => c.CampaignId == campaignId);
        }

        public List<VaccinationCampaign> GetVaccinationCampaignsByCreator(Guid creatorId)
        {
            return _context.VaccinationCampaigns
                .Where(c => c.CreatedBy == creatorId)
                .ToList();
        }

        public List<VaccinationCampaign> SearchVaccinationCampaigns(string searchText)
        {
            searchText = searchText.ToLower();

            return _context.VaccinationCampaigns
                .Where(c => c.IsActive == true && 
                            (c.VaccineName.ToLower().Contains(searchText) || 
                             c.Description.ToLower().Contains(searchText) ||
                             c.CreatedByNavigation.FullName.ToLower().Contains(searchText)))
                .ToList();
        }

        public VaccinationCampaign UpdateVaccinationCampaign(VaccinationCampaign campaign)
        {
            var existingCampaign = _context.VaccinationCampaigns
                .FirstOrDefault(c => c.CampaignId == campaign.CampaignId);
            
            if (existingCampaign != null)
            {
                existingCampaign.VaccineName = campaign.VaccineName;
                existingCampaign.Description = campaign.Description;
                existingCampaign.Date = campaign.Date;
                existingCampaign.CreatedBy = campaign.CreatedBy;
                existingCampaign.StatusId = campaign.StatusId;

                _context.SaveChanges();
            }
            return existingCampaign;
        }
    }
}
