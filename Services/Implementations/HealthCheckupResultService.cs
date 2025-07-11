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
        public List<HealthCheckupResult> getAllHealthCheckupResultByStudentId(int studentId)
        {
            return _healthCheckupResultRepository.GetAllHealthCheckupResultsByStudentId(studentId);
        }
    }
}
