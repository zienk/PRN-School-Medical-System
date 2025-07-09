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
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentService()
        {
            _incidentRepository = new IncidentRepository();
        }

        public async Task AddIncidentAsync(Incident incident)
        {
            if (incident == null)
                throw new ArgumentNullException(nameof(incident));

            if (incident.StudentId <= 0)
                throw new ArgumentException("StudentId không hợp lệ.");

            if (incident.IncidentTypeId <= 0)
                throw new ArgumentException("IncidentTypeId không hợp lệ.");

            if (incident.IncidentDate == default(DateTime))
                throw new ArgumentException("IncidentDate không được để trống.");

            incident.IsActive = true;

            await _incidentRepository.AddIncidentAsync(incident);
        }

        public async Task DeleteIncidentAsync(int incidentId)
        {
            if (incidentId <= 0)
                throw new ArgumentException("IncidentId không hợp lệ.");

            var incident = await _incidentRepository.GetIncidentByIdAsync(incidentId);
            if (incident == null)
                throw new InvalidOperationException("Không tìm thấy sự cố để xóa!");

            await _incidentRepository.DeleteIncidentAsync(incidentId);
        }

        public Task<List<Incident>> GetAllIncidentsAsync()
            => _incidentRepository.GetAllIncidentsAsync();

        public Task<Incident?> GetIncidentByIdAsync(int incidentId)
        {
            if (incidentId <= 0)
                throw new ArgumentException("IncidentId không hợp lệ.");

            return _incidentRepository.GetIncidentByIdAsync(incidentId);
        }

        public Task<List<Incident>> GetIncidentsByStudentIdAsync(int studentId)
        {
            if (studentId <= 0)
                throw new ArgumentException("StudentId không hợp lệ.");

            return _incidentRepository.GetIncidentsByStudentIdAsync(studentId);
        }

        public Task<List<Incident>> SearchIncidentsAsync(string searchText)
            => _incidentRepository.SearchIncidentsAsync(searchText ?? "");

        public async Task UpdateIncidentAsync(Incident incident)
        {
            if (incident == null)
                throw new ArgumentNullException(nameof(incident));

            if (incident.IncidentId <= 0)
                throw new ArgumentException("IncidentId không hợp lệ.");

            var existingIncident = await _incidentRepository.GetIncidentByIdAsync(incident.IncidentId);
            if (existingIncident == null)
                throw new InvalidOperationException("Không tìm thấy sự cố để cập nhật!");

            await _incidentRepository.UpdateIncidentAsync(incident);
        }
    }
}