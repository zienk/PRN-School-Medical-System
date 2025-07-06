using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<List<HealthRecord>> GetAllHealthRecordsAsync()
        {
            return await _contetxt.HealthRecords.Include(e=>e.Student).Where(e => e.IsActive == true)
                .ToListAsync();
        }
    }
}
