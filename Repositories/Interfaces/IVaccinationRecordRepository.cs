using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IVaccinationRecordRepository
    {
        //Thien
        public List<VaccinationRecord> GetAllVaccinationRecordsByStudentId(int studentId);
    }
}
