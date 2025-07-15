using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessObjects.Entities;

namespace Repositories.Interfaces
{
    public interface IHealthRecordRepository
    {
        List<HealthRecord> GetAllHealthRecords();
        List<HealthRecord> getAllMedicalRecordOfStudentByUserId(Guid userId);

        //Thien
        HealthRecord UpdateHealthRecord(HealthRecord healthRecord);

        //Khai báo hồ sức khỏe của học sinh
        HealthRecord AddHealthRecord(HealthRecord healthRecord);
        //Lấy thông tin hồ sơ sức khỏe của học sinh theo id hồ sơ
        HealthRecord GetHealthRecordById(int healthRecordId);
        //Lấy thông tin hồ sơ sức khỏe của học sinh theo id học sinh
        HealthRecord GetHealthRecordByStudentId(int studentId);

        List<HealthRecord> GetAllHealthRecord();


    }
}
