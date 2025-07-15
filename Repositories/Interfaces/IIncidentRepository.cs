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
        //public List<Incident> GetAllIncidents(Guid userId);
        List<Incident> GetAllIncidentsbyUserId(List<Student> students);

        //Delete mềm sự cố theo id của sự cố
        bool SoftDeleteIncident(int incidentId);
        //Tạo mới sự cố
        Incident AddIncident(Incident incident);
        //Cập nhật thông tin sự cố
        Incident UpdateIncident(Incident incident);
        //Lấy sự cố theo id của sự cố
        Incident GetIncidentById(int incidentId);
        //Lấy danh sách sự cố theo id của học sinh
        List<Incident> GetIncidentsByStudentId(int studentId);
        //Lấy danh sách sự cố theo loại sự cố chỉ lấy isActive
        List<Incident> GetIncidentsByIncidentTypeId(int incidentTypeId);
        //Lấy danh sách sự cố theo mức độ nghiêm trọng chỉ lấy isActive
        List<Incident> GetIncidentsBySeverityId(int severityId);
        //Lấy danh sách sự cố theo người báo cáo chỉ lấy isActive
        List<Incident> GetIncidentsByReportedBy(Guid reportedBy);
    }
}
