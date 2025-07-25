using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISeverityLevelRepository
    {
        // Lấy danh sách severity levels để show trong dropdown, combobox
        List<SeverityLevel> GetAllSeverityLevels();
    }
}
