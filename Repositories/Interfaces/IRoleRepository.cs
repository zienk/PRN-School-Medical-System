using BusinessObjects.Entities;
using System.Collections.Generic;


namespace Repositories.Interfaces
{
    public interface IRoleRepository
    {
        // Lấy danh sách role để show trong dropdown, combobox
        List<Role> GetAllRoles();
    }
}
