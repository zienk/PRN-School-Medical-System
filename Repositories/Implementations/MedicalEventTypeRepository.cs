﻿using BusinessObjects.Entities;
using DataAccessLayer;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class MedicalEventTypeRepository : IMedicalEventTypeRepository
    {
        private readonly PrnEduHealthContext _context;

        public MedicalEventTypeRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public List<MedicalEventType> GetAllMedicalEventTypes()
        {
            return _context.MedicalEventTypes
                .ToList();
        }
    }
}