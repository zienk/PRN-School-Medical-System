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
        private readonly IVaccinationRecordRepository vaccinationRecordRepository;

        public VaccinationRecordService()
        {
            this.vaccinationRecordRepository = new VaccinationRecordRepository();
        }


        //Thien
        public List<VaccinationRecord> GetAllVaccinationRecordsByStudentId(int studentId)
        {
            return vaccinationRecordRepository.GetAllVaccinationRecordsByStudentId(studentId);
        }
    }
}
