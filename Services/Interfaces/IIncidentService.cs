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
        //public List<Incident> GetAllIncidents(Guid userId);
        List<Incident> GetAllIncidentsByUserId(List<Student> students);
    }
}
