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
    }
}
