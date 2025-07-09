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
    public class IncidentRepository : IIncidentRepository
    {
        private readonly PrnEduHealthContext _context;

        public IncidentRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public bool AddIncident(Incident incident)
        {
            try
            {
                _context.Incidents.Add(incident);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteIncident(int incidentId)
        {
            try
            {
                var incident = GetIncidentById(incidentId);

                if (incident != null)
                {
                    incident.IsActive = false;
                    _context.Incidents.Update(incident);
                    return _context.SaveChanges() > 0;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public List<Incident> GetAllIncidents()
        {
            try
            {
                return _context.Incidents
                    .Include(i => i.Student)
                    .Include(i => i.IncidentType)
                    .Include(i => i.ReportedByNavigation)
                    .Include(i => i.Severity)
                    .Where(i => i.IsActive == true)
                    .ToList();
            }
            catch
            {
                return new List<Incident>();
            }
        }

        public Incident? GetIncidentById(int incidentId)
        {
            try
            {
                return _context.Incidents
                    .Include(i => i.Student)
                    .Include(i => i.IncidentType)
                    .Include(i => i.ReportedByNavigation)
                    .Include(i => i.Severity)
                    .FirstOrDefault(i => i.IncidentId == incidentId && i.IsActive == true);
            }
            catch
            {
                return null;
            }
        }

        public List<Incident> GetIncidentsByStudentId(int studentId)
        {
            try
            {
                return _context.Incidents
                    .Include(i => i.Student)
                    .Include(i => i.IncidentType)
                    .Include(i => i.ReportedByNavigation)
                    .Include(i => i.Severity)
                    .Where(i => i.StudentId == studentId && i.IsActive == true)
                    .ToList();
            }
            catch
            {
                return new List<Incident>();
            }
        }

        public List<Incident> SearchIncidents(string searchText)
        {
            try
            {
                searchText = searchText.ToLower();

                return _context.Incidents
                    .Include(i => i.Student)
                    .Include(i => i.IncidentType)
                    .Include(i => i.ReportedByNavigation)
                    .Include(i => i.Severity)
                    .Where(i => ((i.Description != null && i.Description.ToLower().Contains(searchText)) ||
                                 (i.Location != null && i.Location.ToLower().Contains(searchText)) ||
                                 (i.ActionsTaken != null && i.ActionsTaken.ToLower().Contains(searchText)) ||
                                 (i.Student.FullName != null && i.Student.FullName.ToLower().Contains(searchText))) &&
                                 i.IsActive == true)
                    .ToList();
            }
            catch
            {
                return new List<Incident>();
            }
        }

        public bool UpdateIncident(Incident incident)
        {
            try
            {
                _context.Incidents.Update(incident);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool IsIncidentExists(int incidentId)
        {
            try
            {
                return _context.Incidents.Any(i => i.IncidentId == incidentId && i.IsActive == true);
            }
            catch
            {
                return false;
            }
        }
    }
}