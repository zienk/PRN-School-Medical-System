using BusinessObjects.Entities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class HealthCheckupResultRepository : IHealthCheckupResultRepository
    {
        private readonly PrnEduHealthContext _context;
        public HealthCheckupResultRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public List<HealthCheckupResult> CreateHealthCheckupResultByHealthCheckupId(List<HealthCheckupResult> healthCheckupResults)
        {
            _context.HealthCheckupResults.AddRange(healthCheckupResults);
            _context.SaveChanges();

            HealthCheckupResult firstHealthCheckupResult = healthCheckupResults.FirstOrDefault();

            HealthCheckup healthCheckupTemp = new HealthCheckup { CheckupId = firstHealthCheckupResult.CheckupId };


            return getAllHealthCheckupResultByHealthCheckupId(healthCheckupTemp);

        }

        public List<HealthCheckupResult> getAllHealthCheckupResultByHealthCheckupId(HealthCheckup healthCheckup)
        {
            return _context.HealthCheckupResults
                                    .Where(c => c.CheckupId == healthCheckup.CheckupId)
                                    .ToList();
        }

        public List<HealthCheckupResult> GetAllHealthCheckupResultsByStudentId(int studentId)
        {
            return _context.HealthCheckupResults
                                    .Include(h => h.Checkup)
                                    .Where(c => c.StudentId == studentId)
                                    .ToList();
        }

        public void UpdateHealthCheckupResult(HealthCheckupResult item)
        {
            var existingItem = _context.HealthCheckupResults
                .FirstOrDefault(h => h.ResultId == item.ResultId);
            _context.HealthCheckupResults.Update(existingItem);
            _context.SaveChanges();
        }
    }
}
