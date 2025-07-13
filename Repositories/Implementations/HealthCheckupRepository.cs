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

        //Upload active = 0;
        public bool RemoveHealthCheckup(HealthCheckup checkup)
        {
            var checkupProgram = _context.HealthCheckups.FirstOrDefault(x => x.CheckupId == checkup.CheckupId);

            if (checkupProgram == null)
                return false;
            if (checkupProgram.IsActive == true)
            {
                checkupProgram.IsActive = false;
                checkupProgram.StatusId = 4; // status = 4 means "Đã hủy"
                _context.HealthCheckups.Update(checkupProgram);
            }
            return _context.SaveChanges() > 0;
        }

        public int GetStatus(HealthCheckup checkupProgram)
        {
            var healthCheckup = _context.HealthCheckups.FirstOrDefault(h => h.CheckupId == checkupProgram.CheckupId);
            return healthCheckup.StatusId;
        }

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

        public bool UpdateStatus(HealthCheckup checkupProgram)
        {
            HealthCheckup healthCheckup = _context.HealthCheckups.FirstOrDefault(h => h.CheckupId == checkupProgram.CheckupId);
            if (healthCheckup == null)
            {
                return false;
            }
            try
            {
                healthCheckup.StatusId = checkupProgram.StatusId;
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