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

        public bool DeleteHealthCheckup(int healthCheckupId)
        {
            try
            {
                var healthCheckup = _context.HealthCheckups.Find(healthCheckupId);
                if (healthCheckup == null) return false;

                _context.HealthCheckups.Remove(healthCheckup);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public List<HealthCheckup> GetAllHealthCheckups()
        => _context.HealthCheckups
            .Include(h => h.CreatedByNavigation)
            .ToList();

        public HealthCheckup? GetHealthCheckupById(int healthCheckupId)
        => _context.HealthCheckups
            .Include(h => h.CreatedByNavigation)
            .FirstOrDefault(h => h.CheckupId == healthCheckupId);

        public List<HealthCheckup> GetHealthCheckupsByCreatorId(Guid creatorId)
        => _context.HealthCheckups
            .Include(h => h.CreatedByNavigation)
            .Where(h => h.CreatedBy == creatorId)
            .ToList();

        public List<HealthCheckup> SearchHealthCheckups(string searchText)
        {
            searchText = searchText.ToLower();

            return _context.HealthCheckups
                .Include(h => h.CreatedByNavigation)
                .Where(h => (h.CheckupName != null && h.CheckupName.ToLower().Contains(searchText)) ||
                            (h.Description != null && h.Description.ToLower().Contains(searchText)))
                .ToList();
        }

        public bool UpdateHealthCheckup(HealthCheckup healthCheckup)
        {
            try
            {
                _context.HealthCheckups.Update(healthCheckup);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}