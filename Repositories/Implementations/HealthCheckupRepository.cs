using DataAccessLayer;
using BusinessObjects.Entities;

using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class HealthCheckupRepository : IHealthCheckupRepository
    {
        private readonly PrnEduHealthContext _context;

        public HealthCheckupRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public bool AddHealthCheckup(HealthCheckup healthCheckup)
        {
            try
            {
                _context.HealthCheckups.Add(healthCheckup);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
