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

        public bool AddIncident(Incident incident)
        {
            if (incident == null)
                return false;

            if (incident.StudentId <= 0)
                return false;

            if (incident.IncidentTypeId <= 0)
                return false;

            if (incident.IncidentDate == default(DateTime))
                return false;

            incident.IsActive = true;

            return _incidentRepository.AddIncident(incident);
        }

        public bool DeleteIncident(int incidentId)
        {
            if (incidentId <= 0)
                return false;

            var incident = _incidentRepository.GetIncidentById(incidentId);
            if (incident == null)
                return false;

            return _incidentRepository.DeleteIncident(incidentId);
        }

        public List<Incident> GetAllIncidents()
        {
            return _incidentRepository.GetAllIncidents();
        }

        public Incident? GetIncidentById(int incidentId)
        {
            if (incidentId <= 0)
                return null;

            return _incidentRepository.GetIncidentById(incidentId);
        }

        public List<Incident> GetIncidentsByStudentId(int studentId)
        {
            if (studentId <= 0)
                return new List<Incident>();

            return _incidentRepository.GetIncidentsByStudentId(studentId);
        }

        public List<Incident> SearchIncidents(string searchText)
        {
            return _incidentRepository.SearchIncidents(searchText ?? "");
        }

        public bool UpdateIncident(Incident incident)
        {
            if (incident == null)
                return false;

            if (incident.IncidentId <= 0)
                return false;

            var existingIncident = _incidentRepository.GetIncidentById(incident.IncidentId);
            if (existingIncident == null)
                return false;

            return _incidentRepository.UpdateIncident(incident);
        }
    }
}