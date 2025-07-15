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

        List<HealthCheckupResult> CreateHealthCheckupResultByHealthCheckupId(List<HealthCheckupResult> healthCheckups);
        List<HealthCheckupResult> getAllHealthCheckupResultByHealthCheckupId(HealthCheckup healthCheckup);
        void UpdateHealthCheckupResult(HealthCheckupResult item);

        // Lấy danh sách kết quả khám sức khỏe theo ID học sinh (để show cho parent)
        List<HealthCheckupResult> GetAllHealthCheckupResultsByStudentId(int studentId);
        // Tạo kết quả khám sức khỏe mới
        HealthCheckupResult AddHealthCheckupResult(HealthCheckupResult healthCheckupResult);
        // Cập nhật kết quả khám sức khỏe

        // Lấy chi tiết kết quả khám sức khỏe theo result ID
        HealthCheckupResult? GetHealthCheckupResultById(int resultId);

    }
}
