using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IHealthCheckupRepository
    {
        // Thêm mới 1 đợt kiểm tra sức khỏe định kỳ
        bool AddHealthCheckup(HealthCheckup healthCheckup);
        // Update thông tin đợt kiểm tra sức khỏe định kỳ
        bool UpdateHealthCheckup(HealthCheckup healthCheckup);
        // Lấy tất cả đợt kiểm tra sức khỏe định kỳ
        List<HealthCheckup> GetAllHealthCheckups();
        // Lấy tất cả đợt kiểm tra sức khỏe định kỳ theo ID người tạo
        List<HealthCheckup> GetHealthCheckupsByCreatorId(Guid creatorId);
        // Lấy thông tin đợt kiểm tra sức khỏe định kỳ theo ID
        HealthCheckup? GetHealthCheckupById(int healthCheckupId);
        // Xóa đợt kiểm tra sức khỏe định kỳ theo ID
        bool DeleteHealthCheckup(int healthCheckupId);
        // Search đợt kiểm tra sức khỏe định kỳ theo tên CheckupName, có thể theo Description nữa
        List<HealthCheckup> SearchHealthCheckups(string searchText);

    }
}
