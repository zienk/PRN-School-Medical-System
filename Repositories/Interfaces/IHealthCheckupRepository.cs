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
        public bool AddHealthCheckup(HealthCheckup healthCheckup);
        // Update thông tin đợt kiểm tra sức khỏe định kỳ
        public bool UpdateHealthCheckup(HealthCheckup healthCheckup);
        // Lấy tất cả đợt kiểm tra sức khỏe định kỳ
        public List<HealthCheckup> GetAllHealthCheckups();
        // Lấy tất cả đợt kiểm tra sức khỏe định kỳ theo ID người tạo
        public List<HealthCheckup> GetHealthCheckupsByCreatorId(Guid creatorId);
        // Lấy thông tin đợt kiểm tra sức khỏe định kỳ theo ID
        public HealthCheckup? GetHealthCheckupById(int healthCheckupId);
        // Xóa đợt kiểm tra sức khỏe định kỳ theo ID
        public bool DeleteHealthCheckup(int healthCheckupId);
        // Search đợt kiểm tra sức khỏe định kỳ theo tên CheckupName, có thể theo Description nữa
        public List<HealthCheckup> SearchHealthCheckups(string searchText);

        //Thien
        bool RemoveHealthCheckup(HealthCheckup checkupProgram);
        //Thien
        public int GetStatus(HealthCheckup checkupProgram);

        //Thien
        bool UpdateStatus(HealthCheckup checkupProgram);
    }
}
