using BusinessObjects.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
namespace Repositories.Implementations
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly PrnEduHealthContext _context;
        public IncidentRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public Incident? AddIncident(Incident incident)
        {
            _context.Incidents.Add(incident);
            _context.SaveChanges();
            return incident;
        }

        public List<Incident> GetAllIncidents()
        {
            return _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.Severity)
                .Include(i => i.ReportedByNavigation)
                    .ThenInclude(u => u.Role)
                .Include(i => i.IncidentType) 
                .Where(i => i.IsActive == true)
                .ToList();
        }


        public List<Incident> GetAllIncidentsbyUserId(List<Student> students)
        {
            var studentIds = students.Select(s => s.StudentId).ToList();
            return _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.Severity)
                .Include(i => i.ReportedByNavigation)
                    .ThenInclude(u => u.Role)
                .Include(i => i.IncidentType)
                .Where(i => i.IsActive == true && studentIds.Contains(i.StudentId))
                .ToList();
        }

        public Incident? GetIncidentById(int incidentId)
        {
            return _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.Severity)
                .Include(i => i.ReportedByNavigation)
                    .ThenInclude(u => u.Role)
                .Include(i => i.IncidentType) 
                .FirstOrDefault(i => i.IncidentId == incidentId && i.IsActive == true);
        }

        public List<Incident> GetIncidentsByIncidentTypeId(int incidentTypeId)
        {
            return _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.Severity)
                .Include(i => i.ReportedByNavigation)
                    .ThenInclude(u => u.Role)
                .Include(i => i.IncidentType) 
                .Where(i => i.IncidentTypeId == incidentTypeId && i.IsActive == true)
                .ToList();
        }

        public List<Incident> GetIncidentsByParentId(Guid parentId)
        {
            return _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.Severity)
                .Include(i => i.ReportedByNavigation)
                    .ThenInclude(u => u.Role)
                .Include(i => i.IncidentType) 
                .Where(i => i.Student.ParentId == parentId && i.IsActive == true)
                .ToList();
        }

        public List<Incident> GetIncidentsByReportedBy(Guid reportedBy)
        {
            return _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.Severity)
                .Include(i => i.ReportedByNavigation)
                    .ThenInclude(u => u.Role)
                .Include(i => i.IncidentType)
                .Where(i => i.ReportedBy == reportedBy && i.IsActive == true)
                .ToList();
        }

        public List<Incident> GetIncidentsBySeverityId(int severityId)
        {
            return _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.Severity)
                .Include(i => i.ReportedByNavigation)
                    .ThenInclude(u => u.Role)
                .Include(i => i.IncidentType)
                .Where(i => i.SeverityId == severityId && i.IsActive == true)
                .ToList();
        }

        public List<Incident> GetIncidentsByStudentId(int studentId)
        {
            return _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.Severity)
                .Include(i => i.ReportedByNavigation)
                    .ThenInclude(u => u.Role)
                .Include(i => i.IncidentType)
                .Where(i => i.StudentId == studentId && i.IsActive == true)
                .ToList();
        }

        public bool HardDeleteIncident(int incidentId)
        {
            var existingIncident = _context.Incidents.FirstOrDefault(i => i.IncidentId == incidentId);

            if (existingIncident == null) return false;

            _context.Incidents.Remove(existingIncident);
            return _context.SaveChanges() > 0;
        }

        public bool SoftDeleteIncident(int incidentId)
        {
            var existingIncident = _context.Incidents.FirstOrDefault(i => i.IncidentId == incidentId);

            if (existingIncident == null) return false;
            existingIncident.IsActive = false;

            _context.Incidents.Update(existingIncident);
            return _context.SaveChanges() > 0;
        }

        public Incident? UpdateIncident(Incident incident)
        {
            var existingIncident = _context.Incidents.FirstOrDefault(i => i.IncidentId == incident.IncidentId);

            if (existingIncident == null) return null;

            existingIncident.IncidentTypeId = incident.IncidentTypeId;
            existingIncident.IncidentDate = incident.IncidentDate;
            existingIncident.Description = incident.Description;
            existingIncident.ActionsTaken = incident.ActionsTaken;
            existingIncident.SeverityId = incident.SeverityId;
            existingIncident.Location = incident.Location;
            existingIncident.IsActive = incident.IsActive;

            _context.Incidents.Update(existingIncident);
            _context.SaveChanges();
            return existingIncident;
        }
    }
}
