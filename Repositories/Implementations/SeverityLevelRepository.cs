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
    public class SeverityLevelRepository : ISeverityLevelRepository
    {
        private readonly PrnEduHealthContext _context;

        public SeverityLevelRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public List<SeverityLevel> GetAllSeverityLevels()
        {
            return _context.SeverityLevels.ToList();
        }
    }
}
