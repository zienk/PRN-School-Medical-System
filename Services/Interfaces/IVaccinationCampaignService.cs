using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IVaccinationCampaignService
    {
        // Tạo mới một chiến dịch tiêm chủng
        VaccinationCampaign AddVaccinationCampaign(VaccinationCampaign campaign);
        // Cập nhật thông tin chiến dịch tiêm chủng
        VaccinationCampaign UpdateVaccinationCampaign(VaccinationCampaign campaign);
        // Xóa chiến dịch tiêm chủng
        // (nên suy nghĩ lại vì xóa chiến dịch này đồng nghĩa với việc xóa tất cả các bản ghi tiêm chủng liên quan)
        bool DeleteVaccinationCampaign(int campaignId);
        // Lấy 1 chiến dịch tiêm chủng theo ID
        VaccinationCampaign? GetVaccinationCampaignById(int campaignId);
        // Lấy tất cả các chiến dịch tiêm chủng
        List<VaccinationCampaign> GetAllVaccinationCampaigns();
        // Lấy tất cả chiến dịch tiêm chủng theo người tạo
        List<VaccinationCampaign> GetVaccinationCampaignsByCreator(Guid creatorId);
        // Search chiến dịch tiêm chủng theo tên vắc xin or mô tả
        List<VaccinationCampaign> SearchVaccinationCampaigns(string searchTerm);
    }
}
