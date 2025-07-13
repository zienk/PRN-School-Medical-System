using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IHealthCheckupResultRepository
    {
        public List<HealthCheckupResult> CreateHealthCheckupResultByHealthCheckupId(List<HealthCheckupResult> healthCheckups);
        List<HealthCheckupResult> getAllHealthCheckupResultByHealthCheckupId(HealthCheckup healthCheckup);
        public List<HealthCheckupResult> GetAllHealthCheckupResultsByStudentId(int studentId);
        void UpdateHealthCheckupResult(HealthCheckupResult item);
    }
}
