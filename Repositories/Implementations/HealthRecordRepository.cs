using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessObjects.Entities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class HealthRecordRepository : IHealthRecordRepository
    {

        PrnEduHealthContext _contetxt;

        public HealthRecordRepository()
        {
            _contetxt = new();
        }
        public List<HealthRecord> GetAllHealthRecords()
        {
            return _contetxt.HealthRecords.Include(e=>e.Student).Where(e => e.IsActive == true)
                .ToList();
        }

        //Thien
        public List<HealthRecord> getAllMedicalRecordOfStudentByUserId(Guid userId)
        {
            return _contetxt.HealthRecords
                .Include(e => e.Student)
                .Include(e => e.Student.Gender)
                .Where(e => e.Student.ParentId == userId && e.IsActive == true) // Added filter
                .ToList();
        } // TODO: Tối ưu hóa các include nếu gặp vấn đề hiệu suất

        //Thien
        public HealthRecord AddRecord(HealthRecord healthRecord)
        {
            healthRecord.CreatedDate = DateTime.Now;
            healthRecord.LastUpdatedDate = DateTime.Now;
            healthRecord.IsActive = true;

            _contetxt.HealthRecords.Add(healthRecord);
            _contetxt.SaveChanges();
            return healthRecord;
        }

        //Thien
        public HealthRecord UpdateHealthRecord(HealthRecord healthRecord)
        {
            HealthRecord record = _contetxt.HealthRecords.FirstOrDefault(x => x.StudentId == healthRecord.StudentId);
            if (record == null)
            {
                // Nếu không tìm thấy bản ghi, tạo mới
                HealthRecord newHealthRecord = AddRecord(healthRecord);
                if (newHealthRecord == null)
                {
                    throw new Exception("Có lỗi trong quá trình thêm!");
                }

                return newHealthRecord;
            }
            record.LastUpdatedDate = DateTime.Now;
            // Cập nhật các trường khác nếu cần
            record.Height = healthRecord.Height;
            record.Weight = healthRecord.Weight;
            record.Allergies = healthRecord.Allergies;
            record.ChronicDiseases = healthRecord.ChronicDiseases;
            record.Notes = healthRecord.Notes;
            _contetxt.HealthRecords.Update(record);
            _contetxt.SaveChanges();
            return record;
        }

        public HealthRecord AddHealthRecord(HealthRecord healthRecord)
        {
            throw new NotImplementedException();
        }

        public HealthRecord GetHealthRecordById(int healthRecordId)
        {
            throw new NotImplementedException();
        }

        public HealthRecord GetHealthRecordByStudentId(int studentId)
        {
            throw new NotImplementedException();
        }
    }
}
