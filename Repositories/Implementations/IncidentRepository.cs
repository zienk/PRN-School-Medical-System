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

        public Incident AddIncident(Incident incident)
        {
            throw new NotImplementedException();
        }

        //public List<Incident> GetAllIncidents(Guid userId)
        //{
        //    return _context.Incidents
        //        .Include(i => i.Student)
        //        .Include(i => i.Severity)
        //        .Include(i => i.ReportedByNavigation)
        //            .ThenInclude(u => u.Role)
        //        .Include(i => i.IncidentType) // Đúng tên property navigation
        //        .Where(i => i.IsActive == true)
        //        .ToList();
        //}

        public List<Incident> GetAllIncidentsbyUserId(List<Student> students)
        {
            var studentIds = students.Select(s => s.StudentId).ToList();
            return _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.Severity)
                .Include(i => i.ReportedByNavigation)
                    .ThenInclude(u => u.Role)
                .Include(i => i.IncidentType) // Đúng tên property navigation
                .Where(i => i.IsActive == true && studentIds.Contains(i.StudentId))
                .ToList();
        }

        public Incident GetIncidentById(int incidentId)
        {
            throw new NotImplementedException();
        }

        public List<Incident> GetIncidentsByIncidentTypeId(int incidentTypeId)
        {
            throw new NotImplementedException();
        }

        public List<Incident> GetIncidentsByReportedBy(Guid reportedBy)
        {
            throw new NotImplementedException();
        }

        public List<Incident> GetIncidentsBySeverityId(int severityId)
        {
            throw new NotImplementedException();
        }

        public List<Incident> GetIncidentsByStudentId(int studentId)
        {
            throw new NotImplementedException();
        }

        public bool SoftDeleteIncident(int incidentId)
        {
            throw new NotImplementedException();
        }

        public Incident UpdateIncident(Incident incident)
        {
            throw new NotImplementedException();
        }
    }
}
