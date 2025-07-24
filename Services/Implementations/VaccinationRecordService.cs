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
            if (vaccinationRecord.StudentId <= 0 || vaccinationRecord.CampaignId <= 0)
            {
                throw new ArgumentException("Dữ liệu bản ghi không hợp lệ");
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


        //Thien
        public List<VaccinationRecord> GetAllVaccinationRecordsByStudentId(int studentId)
        {
            return _vaccinationRecordRepository.GetAllVaccinationRecordsByStudentId(studentId);
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

            return _vaccinationRecordRepository.UpdateVaccinationRecord(vaccinationRecord);
        }
    }
}
