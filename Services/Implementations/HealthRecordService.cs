using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<HealthRecord>> GetAllHealthRecords()
        {
            return  await _healthRecordRepository.GetAllHealthRecordsAsync();
        }
    }
}
