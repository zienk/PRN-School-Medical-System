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

        //public List<Incident> GetAllIncidents(Guid userId)
        //{
        //    return _incidentRepository.GetAllIncidents();
        //}

        public List<Incident> GetAllIncidentsByUserId(List<Student> students)
        {
            return _incidentRepository.GetAllIncidentsbyUserId(students);
        }
    }
}
