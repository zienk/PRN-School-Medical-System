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
    public class MedicalEventTypeService : IMedicalEventTypeService
    {
        private readonly IMedicalEventTypeRepository _medicalEventTypeRepository;

        public MedicalEventTypeService()
        {
            _medicalEventTypeRepository = new MedicalEventTypeRepository();
        }

        public List<MedicalEventType> GetAllMedicalEventTypes()
        {
            return _medicalEventTypeRepository.GetAllMedicalEventTypes();
        }
    }
}
