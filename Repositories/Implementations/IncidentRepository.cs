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

        public async Task AddIncidentAsync(Incident incident)
        {
            await _context.Incidents.AddAsync(incident);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIncidentAsync(int incidentId)
        {
            var incident = await GetIncidentByIdAsync(incidentId);

            if (incident != null)
            {
                incident.IsActive = false;
                _context.Incidents.Update(incident);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<Incident>> GetAllIncidentsAsync()
            => _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.IncidentType)
                .Include(i => i.ReportedByNavigation)
                .Include(i => i.Severity)
                .Where(i => i.IsActive == true)
                .ToListAsync();

        public Task<Incident?> GetIncidentByIdAsync(int incidentId)
            => _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.IncidentType)
                .Include(i => i.ReportedByNavigation)
                .Include(i => i.Severity)
                .FirstOrDefaultAsync(i => i.IncidentId == incidentId && i.IsActive == true);

        public Task<List<Incident>> GetIncidentsByStudentIdAsync(int studentId)
            => _context.Incidents
                .Include(i => i.Student)
                .Include(i => i.IncidentType)
                .Include(i => i.ReportedByNavigation)
                .Include(i => i.Severity)
                .Where(i => i.StudentId == studentId && i.IsActive == true)
                .ToListAsync();

        public Task<List<Incident>> SearchIncidentsAsync(string searchText)
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
                .ToListAsync();
        }

        public async Task UpdateIncidentAsync(Incident incident)
        {
            _context.Incidents.Update(incident);
            await _context.SaveChangesAsync();
        }

        public Task<bool> IsIncidentExistsAsync(int incidentId)
            => _context.Incidents.AnyAsync(i => i.IncidentId == incidentId && i.IsActive == true);
    }
}