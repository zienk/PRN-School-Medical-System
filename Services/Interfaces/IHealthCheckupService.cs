using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Entities;

namespace Services.Interfaces
{
    public interface IHealthCheckupService
    {
        public bool AddHealthCheckup(HealthCheckup healthCheckup);
    }
}
