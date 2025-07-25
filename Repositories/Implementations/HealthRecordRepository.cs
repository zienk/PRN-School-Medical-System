﻿using System;
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

        PrnEduHealthContext _context;

        public HealthRecordRepository()
        {
            _context = new();
        }
        public List<HealthRecord> GetAllHealthRecords()
        {
            return _context.HealthRecords.Include(e => e.Student).Where(e => e.IsActive == true)
                .ToList();
        }

        //Thien
        public List<HealthRecord> getAllMedicalRecordOfStudentByUserId(Guid userId)
        {
            return _context.HealthRecords
                .Include(e => e.Student)
                .Include(e => e.Student.Gender)
                .Where(e => e.Student.ParentId == userId && e.IsActive == true) // Added filter
                .ToList();
        } // TODO: Tối ưu hóa các include nếu gặp vấn đề hiệu suất

        //Thien
        public HealthRecord CreateHealthRecord(HealthRecord healthRecord)
        {
            healthRecord.CreatedDate = DateTime.Now;
            healthRecord.LastUpdatedDate = DateTime.Now;
            healthRecord.IsActive = true;
            _context.HealthRecords.Add(healthRecord);
            _context.SaveChanges();
            return healthRecord;
        }

        //Thien
        public HealthRecord UpdateHealthRecord(HealthRecord healthRecord)
        {
            HealthRecord record = _context.HealthRecords.FirstOrDefault(x => x.StudentId == healthRecord.StudentId);
            if (record == null)
            {
                // Nếu không tìm thấy bản ghi, tạo mới
                HealthRecord newHealthRecord = CreateHealthRecord(healthRecord);
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
            _context.HealthRecords.Update(record);
            _context.SaveChanges();
            return record;
        }

        public HealthRecord GetHealthRecordById(int healthRecordId)
        {
            throw new NotImplementedException();
        }

        public HealthRecord GetHealthRecordByStudentId(int studentId)
        {
            throw new NotImplementedException();
        }


        public List<HealthRecord> GetAllHealthRecord()
        {
            return _context.HealthRecords
                .Include(e => e.Student).ThenInclude(g => g.Gender)
                .Include(e => e.Student).ThenInclude(p=> p.Parent)
                .Where(e => e.IsActive == true)
                .ToList();
        }
    }
}