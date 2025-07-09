using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IIncidentService
    {
        Task<Incident?> GetIncidentByIdAsync(int incidentId);
        Task<List<Incident>> GetAllIncidentsAsync();
        Task<List<Incident>> GetIncidentsByStudentIdAsync(int studentId);
        Task<List<Incident>> SearchIncidentsAsync(string searchText);
        Task AddIncidentAsync(Incident incident);
        Task UpdateIncidentAsync(Incident incident);
        Task DeleteIncidentAsync(int incidentId);
    }
}