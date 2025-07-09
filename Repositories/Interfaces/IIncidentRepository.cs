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
        Incident? GetIncidentById(int incidentId);
        List<Incident> GetAllIncidents();
        List<Incident> GetIncidentsByStudentId(int studentId);
        List<Incident> SearchIncidents(string searchText);
        bool AddIncident(Incident incident);
        bool UpdateIncident(Incident incident);
        bool DeleteIncident(int incidentId);
        bool IsIncidentExists(int incidentId);
    }
}