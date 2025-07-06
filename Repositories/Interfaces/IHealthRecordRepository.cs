using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Entities;

namespace Repositories.Interfaces
{
    public interface IHealthRecordRepository
    {
        Task<List<HealthRecord>> GetAllHealthRecordsAsync();
    }
}
