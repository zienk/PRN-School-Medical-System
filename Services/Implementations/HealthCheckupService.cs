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
    public class HealthCheckupService : IHealthCheckupService
    {
        private readonly IHealthCheckupRepository _healthCheckupRepository;

        public HealthCheckupService()
        {
            _healthCheckupRepository = new HealthCheckupRepository();
        }

        public bool AddHealthCheckup(HealthCheckup healthCheckup)
        {
            return _healthCheckupRepository.AddHealthCheckup(healthCheckup);
        }
    }
}
