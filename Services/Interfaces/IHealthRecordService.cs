﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface IHealthRecordService
    {
        List<HealthRecord> GetAllHealthRecords();
    }
}
