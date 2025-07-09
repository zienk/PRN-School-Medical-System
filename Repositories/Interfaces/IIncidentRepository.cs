using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IIncidentRepository
    {
        Task<Incident?> GetIncidentByIdAsync(int incidentId);
        Task<List<Incident>> GetAllIncidentsAsync();
        Task<List<Incident>> GetIncidentsByStudentIdAsync(int studentId);
        Task<List<Incident>> SearchIncidentsAsync(string searchText);
        Task AddIncidentAsync(Incident incident);
        Task UpdateIncidentAsync(Incident incident);
        Task DeleteIncidentAsync(int incidentId);
        Task<bool> IsIncidentExistsAsync(int incidentId);
    }
}