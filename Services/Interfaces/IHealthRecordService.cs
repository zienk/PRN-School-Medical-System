using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface IHealthRecordService
    {
        Task<List<HealthRecord>> GetAllHealthRecords();
    }
}
