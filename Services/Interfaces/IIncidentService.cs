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
        //Thiên - Lấy tất cả sự cố của học sinh theo danh sách học sinh
        List<Incident> GetAllIncidentsbyUserId(List<Student> students);


        //Xóa cứng sự cố theo id của sự cố
        bool HardDeleteIncident(int incidentId);
        //Delete mềm sự cố theo id của sự cố
        bool SoftDeleteIncident(int incidentId);
        //Tạo mới sự cố
        Incident? AddIncident(Incident incident);
        //Cập nhật thông tin sự cố
        Incident? UpdateIncident(Incident incident);
        //Lấy sự cố theo id của sự cố
        Incident? GetIncidentById(int incidentId);
        //Lấy danh sách sự cố theo id của học sinh (lịch sử sự cố của học sinh đó) chỉ lấy isActive
        List<Incident> GetIncidentsByStudentId(int studentId);
        //Lấy danh sách sự cố theo loại sự cố chỉ lấy isActive
        List<Incident> GetIncidentsByIncidentTypeId(int incidentTypeId);
        //Lấy danh sách sự cố theo mức độ nghiêm trọng chỉ lấy isActive
        List<Incident> GetIncidentsBySeverityId(int severityId);
        //Lấy danh sách sự cố theo người báo cáo chỉ lấy isActive
        List<Incident> GetIncidentsByReportedBy(Guid reportedBy);
        
        //Lấy danh sách tất cả sự cố
        List<Incident> GetAllIncidents();

        //Lấy danh sách sự cố theo id của phụ huynh
        List<Incident> GetIncidentsByParentId(Guid parentId);
    }
}
