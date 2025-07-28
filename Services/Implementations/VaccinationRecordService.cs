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
    public class VaccinationRecordService : IVaccinationRecordService
    {
        private readonly IVaccinationRecordRepository _vaccinationRecordRepository;

        public VaccinationRecordService()
        {
            _vaccinationRecordRepository = new VaccinationRecordRepository();
        }

        public VaccinationRecord AddVaccinationRecord(VaccinationRecord vaccinationRecord)
        {
            if (vaccinationRecord == null)
            {
                throw new ArgumentNullException(nameof(vaccinationRecord), "Dữ liệu bản ghi không được để trống");
            }
            
            // Validate basic data
            if (vaccinationRecord.StudentId <= 0 || vaccinationRecord.CampaignId <= 0)
            {
                throw new ArgumentException("Dữ liệu bản ghi không hợp lệ");
            }

            // Validate vaccination date
            if (vaccinationRecord.VaccinationDate > DateTime.Now)
            {
                throw new ArgumentException("Ngày tiêm không thể trong tương lai");
            }

            // Check for duplicate records
            var existingRecords = _vaccinationRecordRepository.GetVaccinationRecordsByCampaignId(vaccinationRecord.CampaignId.Value);
            if (existingRecords.Any(r => r.StudentId == vaccinationRecord.StudentId && r.IsActive == true))
            {
                throw new InvalidOperationException("Học sinh này đã có bản ghi tiêm chủng cho chiến dịch này");
            }

            // Validate campaign eligibility (campaign should be active)
            var campaignService = new VaccinationCampaignService();
            var campaign = campaignService.GetVaccinationCampaignById(vaccinationRecord.CampaignId.Value);
            if (campaign == null || campaign.IsActive != true)
            {
                throw new InvalidOperationException("Chiến dịch tiêm chủng không tồn tại hoặc không còn hoạt động");
            }

            return _vaccinationRecordRepository.AddVaccinationRecord(vaccinationRecord);
        }

        public bool DeleteVaccinationRecord(int vaccinationRecordId)
        {
            var existingRecord = _vaccinationRecordRepository.GetVaccinationRecordById(vaccinationRecordId);
            if (existingRecord == null)
            {
                throw new InvalidOperationException("Không tìm thấy bản ghi tiêm chủng để xóa.");
            }

            return _vaccinationRecordRepository.DeleteVaccinationRecord(vaccinationRecordId);
        }


        //Thien - Enhanced with sorting by date for better UX
        public List<VaccinationRecord> GetAllVaccinationRecordsByStudentId(int studentId)
        {
            var records = _vaccinationRecordRepository.GetAllVaccinationRecordsByStudentId(studentId);
            return records.OrderByDescending(r => r.VaccinationDate).ToList();
        }

        // Method to get all vaccination records
        public List<VaccinationRecord> GetAllVaccinationRecords()
        {
            var allRecords = new List<VaccinationRecord>();
            var campaignService = new VaccinationCampaignService();
            var allCampaigns = campaignService.GetAllVaccinationCampaigns();
            
            foreach (var campaign in allCampaigns)
            {
                var campaignRecords = _vaccinationRecordRepository.GetVaccinationRecordsByCampaignId(campaign.CampaignId);
                allRecords.AddRange(campaignRecords);
            }
            
            return allRecords.OrderByDescending(r => r.VaccinationDate).ToList();
        }

        public VaccinationRecord? GetVaccinationRecordById(int vaccinationRecordId)
        {
            return _vaccinationRecordRepository.GetVaccinationRecordById(vaccinationRecordId);
        }

        public List<VaccinationRecord> GetVaccinationRecordsByCampaignId(int campaignId)
        {
            return _vaccinationRecordRepository.GetVaccinationRecordsByCampaignId(campaignId);
        }

        public List<VaccinationRecord> SearchVaccinationRecords(string searchText)
        {
            return _vaccinationRecordRepository.SearchVaccinationRecords(searchText);
        }

        public VaccinationRecord? UpdateVaccinationRecord(VaccinationRecord vaccinationRecord)
        {
            if (vaccinationRecord == null || vaccinationRecord.VaccinationRecordId <= 0)
            {
                throw new ArgumentException("Dữ liệu bản ghi không hợp lệ");
            }

            var existingRecord = _vaccinationRecordRepository.GetVaccinationRecordById(vaccinationRecord.VaccinationRecordId);

            if (existingRecord == null)
            {
                throw new InvalidOperationException("Không tìm thấy bản ghi tiêm chủng để cập nhật.");
            }

            // Validate basic data
            if (vaccinationRecord.StudentId <= 0 || vaccinationRecord.CampaignId <= 0)
            {
                throw new ArgumentException("Dữ liệu bản ghi không hợp lệ");
            }

            // Validate vaccination date
            if (vaccinationRecord.VaccinationDate > DateTime.Now)
            {
                throw new ArgumentException("Ngày tiêm không thể trong tương lai");
            }

            // Check for duplicate records (only if student or campaign changed)
            if (existingRecord.StudentId != vaccinationRecord.StudentId || 
                existingRecord.CampaignId != vaccinationRecord.CampaignId)
            {
                var existingRecords = _vaccinationRecordRepository.GetVaccinationRecordsByCampaignId(vaccinationRecord.CampaignId.Value);
                if (existingRecords.Any(r => r.StudentId == vaccinationRecord.StudentId && 
                                           r.VaccinationRecordId != vaccinationRecord.VaccinationRecordId && 
                                           r.IsActive == true))
                {
                    throw new InvalidOperationException("Học sinh này đã có bản ghi tiêm chủng cho chiến dịch này");
                }
            }

            // Validate campaign eligibility
            var campaignService = new VaccinationCampaignService();
            var campaign = campaignService.GetVaccinationCampaignById(vaccinationRecord.CampaignId.Value);
            if (campaign == null || campaign.IsActive != true)
            {
                throw new InvalidOperationException("Chiến dịch tiêm chủng không tồn tại hoặc không còn hoạt động");
            }

            return _vaccinationRecordRepository.UpdateVaccinationRecord(vaccinationRecord);
        }

        public bool CreateVaccinationRecordsForCampaign(int campaignId, List<int> studentIds)
        {
            if (studentIds == null || !studentIds.Any())
            {
                throw new ArgumentException("Danh sách học sinh không được để trống");
            }

            // Validate campaign exists and is active
            var campaignService = new VaccinationCampaignService();
            var campaign = campaignService.GetVaccinationCampaignById(campaignId);
            if (campaign == null || campaign.IsActive != true)
            {
                throw new InvalidOperationException("Chiến dịch tiêm chủng không tồn tại hoặc không còn hoạt động");
            }

            // Get existing records for this campaign to avoid duplicates
            var existingRecords = _vaccinationRecordRepository.GetVaccinationRecordsByCampaignId(campaignId);
            var existingStudentIds = existingRecords.Where(r => r.IsActive == true).Select(r => r.StudentId).ToList();

            int createdCount = 0;
            foreach (var studentId in studentIds)
            {
                // Skip if record already exists for this student in this campaign
                if (existingStudentIds.Contains(studentId))
                    continue;

                var vaccinationRecord = new VaccinationRecord
                {
                    StudentId = studentId,
                    CampaignId = campaignId,
                    VaccinationDate = null, // Chưa tiêm, sẽ cập nhật sau
                    Result = null, // Chưa có kết quả
                    Notes = "Đã lên danh sách - chưa tiêm",
                    IsActive = true
                };

                try
                {
                    _vaccinationRecordRepository.AddVaccinationRecord(vaccinationRecord);
                    createdCount++;
                }
                catch (Exception ex)
                {
                    // Log error but continue with other students
                    Console.WriteLine($"Error creating vaccination record for student {studentId}: {ex.Message}");
                }
            }

            return createdCount > 0;
        }

        public bool UpdateVaccinationResult(int vaccinationRecordId, string result, string notes = null)
        {
            var existingRecord = _vaccinationRecordRepository.GetVaccinationRecordById(vaccinationRecordId);
            if (existingRecord == null || existingRecord.IsActive != true)
            {
                throw new InvalidOperationException("Không tìm thấy bản ghi tiêm chủng để cập nhật.");
            }

            // Update the record with vaccination result
            existingRecord.VaccinationDate = DateTime.Now;
            existingRecord.Result = result;
            existingRecord.Notes = notes ?? existingRecord.Notes;

            var updatedRecord = _vaccinationRecordRepository.UpdateVaccinationRecord(existingRecord);
            return updatedRecord != null;
        }
    }
}
