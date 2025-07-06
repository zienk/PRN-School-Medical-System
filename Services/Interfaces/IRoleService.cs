using BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRolesAsync();
    }
}
