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
    public class VaccinationRecordRepository : IVaccinationRecordRepository
    {
        private readonly PrnEduHealthContext _context;
        public VaccinationRecordRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public VaccinationRecord AddVaccinationRecord(VaccinationRecord vaccinationRecord)
        {
            _context.VaccinationRecords.Add(vaccinationRecord);
            _context.SaveChanges();
            return vaccinationRecord;
        }

        public bool DeleteVaccinationRecord(int vaccinationRecordId)
        {
            var exsistingRecord = _context.VaccinationRecords
                .FirstOrDefault(v => v.VaccinationRecordId == vaccinationRecordId);

            if (exsistingRecord == null) return false;

            _context.VaccinationRecords.Remove(exsistingRecord);
            return _context.SaveChanges() > 0;
        }

        //Thien
        public List<VaccinationRecord> GetAllVaccinationRecordsByStudentId(int studentId)
        {
            return _context.VaccinationRecords
                .Include(v => v.Student)
                .Include(v => v.Campaign)
                .Where(v => v.StudentId == studentId && v.IsActive == true) // Added filter
                .ToList();
        } // TODO: Thêm sắp xếp theo VaccinationDate để UX tốt hơn"

        public VaccinationRecord? GetVaccinationRecordById(int vaccinationRecordId)
        {
            return _context.VaccinationRecords
                .Include(v => v.Student)
                .Include(v => v.Campaign)
                .FirstOrDefault(v => v.VaccinationRecordId == vaccinationRecordId);
        }

        public List<VaccinationRecord> GetVaccinationRecordsByCampaignId(int campaignId)
        {
            return _context.VaccinationRecords
                .Include(v => v.Student)
                .Include(v => v.Campaign)
                .Where(v => v.CampaignId == campaignId && v.IsActive == true) 
                .ToList();
        }

        public List<VaccinationRecord> SearchVaccinationRecords(string searchText)
        {
            searchText = searchText.ToLower();

            return _context.VaccinationRecords
                .Include(v => v.Student)
                .Include(v => v.Campaign)
                .Where(v => (v.Student.FullName.ToLower().Contains(searchText) ||
                             v.Campaign.VaccineName.ToLower().Contains(searchText)) ||
                             v.Result.ToLower().Contains(searchText) ||
                             v.Notes.ToLower().Contains(searchText) && 
                             v.IsActive == true)
                .ToList();

        }

        public VaccinationRecord? UpdateVaccinationRecord(VaccinationRecord vaccinationRecord)
        {
            var exstingRecord = _context.VaccinationRecords
                .FirstOrDefault(v => v.VaccinationRecordId == vaccinationRecord.VaccinationRecordId);

            if (exstingRecord != null)
            {
                exstingRecord.StudentId = vaccinationRecord.StudentId;
                exstingRecord.CampaignId = vaccinationRecord.CampaignId;
                exstingRecord.VaccinationDate = vaccinationRecord.VaccinationDate;
                exstingRecord.Result = vaccinationRecord.Result;
                exstingRecord.Notes = vaccinationRecord.Notes;
                exstingRecord.IsActive = vaccinationRecord.IsActive;
                _context.SaveChanges();
            }
            return exstingRecord;
        }
    }
}
