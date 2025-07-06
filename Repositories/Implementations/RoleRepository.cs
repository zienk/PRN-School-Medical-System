using BusinessObjects.Entities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly PrnEduHealthContext _context;

        public RoleRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}
