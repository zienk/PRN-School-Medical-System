using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IVaccinationRecordService
    {
        //Thien - Lấy danh sách bản ghi tiêm chủng theo id học sinh (lịch sử tiêm chủng của 1hs)
        List<VaccinationRecord> GetAllVaccinationRecordsByStudentId(int studentId);

        //Lấy danh sách bản ghi tiêm chủng theo đợt tiêm chủng
        List<VaccinationRecord> GetVaccinationRecordsByCampaignId(int campaignId);

        //Thông tin chi tiết bản ghi tiêm chủng theo id bản ghi
        VaccinationRecord? GetVaccinationRecordById(int vaccinationRecordId);

        //Thêm mới 1 bản ghi tiêm chủng
        VaccinationRecord AddVaccinationRecord(VaccinationRecord vaccinationRecord);
        // Cập nhật thông tin bản ghi tiêm chủng
        VaccinationRecord? UpdateVaccinationRecord(VaccinationRecord vaccinationRecord);
        // Xóa bản ghi tiêm chủng
        bool DeleteVaccinationRecord(int vaccinationRecordId);
        //Search bản ghi tiêm chủng theo nhiều tiêu chí
        List<VaccinationRecord> SearchVaccinationRecords(string searchText);

        //Lấy tất cả bản ghi tiêm chủng
        List<VaccinationRecord> GetAllVaccinationRecords();

        //Thống kê số lượng bản ghi tiêm chủng thành công | thất bại (Todo)
        
        // Tạo danh sách VaccinationRecord cho các học sinh được chọn trong chiến dịch
        bool CreateVaccinationRecordsForCampaign(int campaignId, List<int> studentIds);
        
        // Cập nhật kết quả tiêm chủng (khi thực sự tiêm)
        bool UpdateVaccinationResult(int vaccinationRecordId, string result, string notes = null);
    }
}
