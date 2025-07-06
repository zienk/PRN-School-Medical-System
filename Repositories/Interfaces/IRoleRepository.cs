using BusinessObjects.Entities;
using System.Collections.Generic;


namespace Repositories.Interfaces
{
    public interface IRoleRepository
    {
        List<Role> GetAllRoles();
    }
}
