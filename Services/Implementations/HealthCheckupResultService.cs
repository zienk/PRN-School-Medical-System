using BusinessObjects.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Interfaces;
using Repositories.Implementations;
namespace Services.Implementations
{
    public class HealthCheckupResultService: IHealthCheckupResultService
    {
        private readonly IHealthCheckupResultRepository _healthCheckupResultRepository;
        public HealthCheckupResultService()
        {
            _healthCheckupResultRepository = new HealthCheckupResultRepository();
        }

        public List<HealthCheckupResult> CreateHealthCheckupResultByHealthCheckupId(List<HealthCheckupResult> healthCheckupResults)
        {
            return _healthCheckupResultRepository.CreateHealthCheckupResultByHealthCheckupId(healthCheckupResults);
        }

        //Thien
        public List<HealthCheckupResult> getAllHealthCheckupResultByHealthCheckupId(HealthCheckup healthCheckup)
        {
            return _healthCheckupResultRepository.getAllHealthCheckupResultByHealthCheckupId(healthCheckup);
        }

        //Thien
        public List<HealthCheckupResult> getAllHealthCheckupResultByStudentId(int studentId)
        {
            return _healthCheckupResultRepository.GetAllHealthCheckupResultsByStudentId(studentId);
        }

        //Thien
        public void UpdateHealthCheckupResult(HealthCheckupResult item)
        {
            _healthCheckupResultRepository.UpdateHealthCheckupResult(item);
        }
    }
}
