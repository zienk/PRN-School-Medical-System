using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IHealthCheckupResultRepository
    {
        // Lấy danh sách kết quả khám sức khỏe theo ID học sinh (để show cho parent)
        List<HealthCheckupResult> GetAllHealthCheckupResultsByStudentId(int studentId);
        // Tạo kết quả khám sức khỏe mới
         
    }
}
