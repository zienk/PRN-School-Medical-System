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
        public List<HealthCheckupResult> GetAllHealthCheckupResultsByStudentId(int studentId)
        {
            return _context.HealthCheckupResults
                                    .Include(h => h.Checkup)
                                    .Where(c => c.StudentId == studentId)
                                    .ToList();
        }
    }
}
