using BusinessObjects.Entities;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class SeverityLevelService : ISeverityLevelService
    {
        private readonly ISeverityLevelRepository _severityLevelRepository;

        public SeverityLevelService()
        {
            _severityLevelRepository = new SeverityLevelRepository();
        }

        public List<SeverityLevel> GetAllSeverityLevels()
        {
            return _severityLevelRepository.GetAllSeverityLevels();
        }
    }
}
