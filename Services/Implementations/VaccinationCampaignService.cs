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
    public class VaccinationCampaignService : IVaccinationCampaignService
    {
        private readonly IVaccinationCampaignRepository _vaccinationCampaignRepository;

        public VaccinationCampaignService()
        {
            _vaccinationCampaignRepository = new VaccinationCampaignRepository();
        }

        public VaccinationCampaign AddVaccinationCampaign(VaccinationCampaign campaign)
        {
            return _vaccinationCampaignRepository.AddVaccinationCampaign(campaign);
        }

        public bool DeleteVaccinationCampaign(int campaignId)
        {
            return _vaccinationCampaignRepository.DeleteVaccinationCampaign(campaignId);
        }

        public List<VaccinationCampaign> GetAllVaccinationCampaigns()
        {
            return _vaccinationCampaignRepository.GetAllVaccinationCampaigns();
        }

        public VaccinationCampaign? GetVaccinationCampaignById(int campaignId)
        {
            return _vaccinationCampaignRepository.GetVaccinationCampaignById(campaignId);
        }

        public List<VaccinationCampaign> GetVaccinationCampaignsByCreator(Guid creatorId)
        {
            return _vaccinationCampaignRepository.GetVaccinationCampaignsByCreator(creatorId);
        }

        public List<VaccinationCampaign> SearchVaccinationCampaigns(string searchTerm)
        {
            return _vaccinationCampaignRepository.SearchVaccinationCampaigns(searchTerm);
        }

        public VaccinationCampaign UpdateVaccinationCampaign(VaccinationCampaign campaign)
        {
            if (campaign == null)
                throw new ArgumentNullException(nameof(campaign), "Campaign không được để trống.");

            var existingCampaign = _vaccinationCampaignRepository.GetVaccinationCampaignById(campaign.CampaignId);
            if (existingCampaign == null)
                throw new InvalidOperationException("Không tìm thấy chiến dịch tiêm chủng để cập nhật.");

            return _vaccinationCampaignRepository.UpdateVaccinationCampaign(campaign);
        }
    }
}
