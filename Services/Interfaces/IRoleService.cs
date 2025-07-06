using BusinessObjects.Entities;
using System.Collections.Generic;


namespace Services.Interfaces
{
    public interface IRoleService
    {
        List<Role> GetAllRoles();
    }
}
