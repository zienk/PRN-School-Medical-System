using BusinessObjects.Entities;
using DataAccessLayer;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class GenderTypeRepository : IGenderTypeRepository
    {
        private readonly PrnEduHealthContext _context;

        public GenderTypeRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public List<GenderType> GetAllGenderTypes()
        => _context.GenderTypes.ToList();

    }
}
