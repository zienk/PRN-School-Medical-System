using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IGenderTypeRepository
    {
        // Lấy danh sách gender types để show trong dropdown, combobox
        List<GenderType> GetAllGenderTypes();
    }
}
