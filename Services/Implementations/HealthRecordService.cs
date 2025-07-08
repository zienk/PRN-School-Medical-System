using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessObjects.Entities;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Implementations
{
    public class HealthRecordService : IHealthRecordService
    {
        private readonly IHealthRecordRepository _healthRecordRepository;

        public HealthRecordService()
        {
            _healthRecordRepository = new HealthRecordRepository();
        }

        public List<HealthRecord> GetAllHealthRecords()
        {
            return  _healthRecordRepository.GetAllHealthRecords();
        }

        public List<HealthRecord> getAllMedicalRecordOfStudentByUserId(Guid userId)
        {
            return _healthRecordRepository.getAllMedicalRecordOfStudentByUserId(userId);
        }

        //Thien
        public HealthRecord UpdateHealthRecord(HealthRecord healthRecord)
        {
            return _healthRecordRepository.UpdateHealthRecord(healthRecord);
        }
    }
}
