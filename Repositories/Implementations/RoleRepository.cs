using BusinessObjects.Entities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System.Collections.Generic;


namespace Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly PrnEduHealthContext _context;

        public RoleRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
