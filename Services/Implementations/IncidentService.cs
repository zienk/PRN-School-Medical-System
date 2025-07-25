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

        public Incident? AddIncident(Incident incident)
        {
            if (incident == null)
            {
                throw new ArgumentNullException(nameof(incident), "Dữ liệu sự cố y tế không hợp lệ");
            }
            if (string.IsNullOrWhiteSpace(incident.Description))
            {
                throw new ArgumentException("Mô tả sự cố không được để trống", nameof(incident.Description));
            }
            if (incident.ReportedBy == Guid.Empty)
            {
                throw new ArgumentException("Người báo cáo không được để trống", nameof(incident.ReportedBy));
            }
            if (incident.IncidentDate == default)
            {
                throw new ArgumentException("Ngày báo cáo không hợp lệ", nameof(incident.IncidentDate));
            }
            if (incident.IncidentDate > DateTime.Now)
            {
                throw new ArgumentException("Ngày báo cáo không thể trong tương lai", nameof(incident.IncidentDate));
            }
            
            return _incidentRepository.AddIncident(incident);

        }

        public List<Incident> GetAllIncidents()
        {
            return _incidentRepository.GetAllIncidents();
        }


        //Thiên - Lấy tất cả sự cố của học sinh theo danh sách học sinh
        public List<Incident> GetAllIncidentsbyUserId(List<Student> students)
        {
            return _incidentRepository.GetAllIncidentsbyUserId(students);
        }

        public Incident? GetIncidentById(int incidentId)
        {
            if (incidentId <= 0)
            {
                throw new ArgumentException("Mã sự cố không hợp lệ", nameof(incidentId));
            }
            return _incidentRepository.GetIncidentById(incidentId);
        }

        public List<Incident> GetIncidentsByIncidentTypeId(int incidentTypeId)
        {
            return _incidentRepository.GetIncidentsByIncidentTypeId(incidentTypeId);
        }

        public List<Incident> GetIncidentsByParentId(Guid parentId)
        {
            if (parentId == Guid.Empty)
            {
                throw new ArgumentException("Mã phụ huynh không hợp lệ", nameof(parentId));
            }
            return _incidentRepository.GetIncidentsByParentId(parentId);
        }

        public List<Incident> GetIncidentsByReportedBy(Guid reportedBy)
        {
            if (reportedBy == Guid.Empty)
            {
                throw new ArgumentException("Người báo cáo không được để trống", nameof(reportedBy));
            }
            return _incidentRepository.GetIncidentsByReportedBy(reportedBy);
        }

        public List<Incident> GetIncidentsBySeverityId(int severityId)
        {
            if (severityId <= 0)
            {
                throw new ArgumentException("Mã mức độ nghiêm trọng không hợp lệ", nameof(severityId));
            }
            return _incidentRepository.GetIncidentsBySeverityId(severityId);
        }

        public List<Incident> GetIncidentsByStudentId(int studentId)
        {
            if (studentId <= 0)
            {
                throw new ArgumentException("Mã học sinh không hợp lệ", nameof(studentId));
            }
            return _incidentRepository.GetIncidentsByStudentId(studentId);
        }

        public bool HardDeleteIncident(int incidentId)
        {
            if (incidentId <= 0)
            {
                throw new ArgumentException("Mã sự cố không hợp lệ", nameof(incidentId));
            }
            var existingIncident = _incidentRepository.GetIncidentById(incidentId);
            if (existingIncident == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy sự cố với mã {incidentId}");
            }
            return _incidentRepository.HardDeleteIncident(incidentId);
        }

        public bool SoftDeleteIncident(int incidentId)
        {
            if (incidentId <= 0)
            {
                throw new ArgumentException("Mã sự cố không hợp lệ", nameof(incidentId));
            }
            var existingIncident = _incidentRepository.GetIncidentById(incidentId);
            if (existingIncident == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy sự cố với mã {incidentId}");
            }
            

            return _incidentRepository.SoftDeleteIncident(incidentId);
        }

        public Incident? UpdateIncident(Incident incident)
        {
            // Nếu người dùng không nhập gì thì nên giữ nguyên dữ liệu cũ (todo)

            return _incidentRepository.UpdateIncident(incident);
        }
    }
}
