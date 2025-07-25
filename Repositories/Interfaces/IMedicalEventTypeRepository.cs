using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IMedicalEventTypeRepository
    {
        // Lấy danh sách medical event types để show trong dropdown, combobox
        List<MedicalEventType> GetAllMedicalEventTypes();
    }
}
