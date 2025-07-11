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
    public class GenderTypeService : IGenderTypeService
    {
        private readonly IGenderTypeRepository _genderTypeRepository;

        public GenderTypeService()
        {
            _genderTypeRepository = new GenderTypeRepository();
        }

        public List<GenderType> GetAllGenderTypes()
        {
            throw new NotImplementedException();
        }
    }
}
