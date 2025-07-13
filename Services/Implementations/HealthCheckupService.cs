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

        public bool DeleteHealthCheckup(int healthCheckupId)
        {
            var existingCheckup = _healthCheckupRepository.GetHealthCheckupById(healthCheckupId);
            
            if (existingCheckup == null)
                return false;

            return _healthCheckupRepository.DeleteHealthCheckup(healthCheckupId);
        }

        public List<HealthCheckup> GetAllHealthCheckups()
        => _healthCheckupRepository.GetAllHealthCheckups();

        public HealthCheckup? GetHealthCheckupById(int healthCheckupId)
        {
            if (healthCheckupId <= 0)
                return null;

            return _healthCheckupRepository.GetHealthCheckupById(healthCheckupId);
        }

        public List<HealthCheckup> GetHealthCheckupsByCreatorId(Guid creatorId)
        => _healthCheckupRepository.GetHealthCheckupsByCreatorId(creatorId);


        public List<HealthCheckup> SearchHealthCheckups(string searchText)
        => _healthCheckupRepository.SearchHealthCheckups(searchText ?? "");

        public bool UpdateHealthCheckup(HealthCheckup healthCheckup)
        {
            if (healthCheckup == null || healthCheckup.CheckupId <= 0)
                return false;
            
            var existingCheckup = _healthCheckupRepository.GetHealthCheckupById(healthCheckup.CheckupId);

            if (existingCheckup == null)
                return false;

            existingCheckup.CheckupName = healthCheckup.CheckupName;
            existingCheckup.CheckupDate = healthCheckup.CheckupDate;
            existingCheckup.Description = healthCheckup.Description;

            return _healthCheckupRepository.UpdateHealthCheckup(existingCheckup);
        }

        //Thien : Get the status of a health checkup
        public int GetStatus(HealthCheckup checkupProgram)
        {
            return _healthCheckupRepository.GetStatus(checkupProgram);
        }

        //Thien: Remove health checkup by setting IsActive to false
        public bool RemoveHealthCheckup(HealthCheckup checkupProgram)
        {
            return _healthCheckupRepository.RemoveHealthCheckup(checkupProgram);
        }

        public bool UpdateStatus(HealthCheckup checkupProgram)
        {
            return _healthCheckupRepository.UpdateStatus(checkupProgram);
        }
    }
}
