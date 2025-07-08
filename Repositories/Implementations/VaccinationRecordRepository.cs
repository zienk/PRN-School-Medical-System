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

        //Thien
        public List<VaccinationRecord> GetAllVaccinationRecordsByStudentId(int studentId)
        {
            return _context.VaccinationRecords
                .Include(v => v.Student)
                .Include(v => v.Campaign)
                .Where(v => v.StudentId == studentId)
                .ToList();
        }
    }
}
