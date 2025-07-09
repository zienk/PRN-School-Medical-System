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
    }
}
