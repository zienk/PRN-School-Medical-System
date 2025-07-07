<<<<<<< Updated upstream
﻿using System;
=======
﻿using BusinessObjects.Entities;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
>>>>>>> Stashed changes
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< Updated upstream
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Repositories.Implementations;
using Services.Interfaces;
=======
>>>>>>> Stashed changes

namespace Services.Implementations
{
    public class StudentService : IStudentService
    {
<<<<<<< Updated upstream
        private StudentRepository _studentRepository;


=======
        private readonly IStudentRepository _studentRepository;
>>>>>>> Stashed changes
        public StudentService()
        {
            _studentRepository = new StudentRepository();
        }
<<<<<<< Updated upstream

        public void AddStudent(Student student)
        {
            _studentRepository.AddStudent(student);
        }

        public void DeleteStudent(Guid studentId)
        {
            _studentRepository.DeleteStudent(studentId);
        }

        public List<Student> GetAllStudents()
        {
           return _studentRepository.GetAllStudents();
        }

        public List<Student> SearchStudents(string searchTerm)
        {
          return  _studentRepository.SearchStudents(searchTerm);
        }

        public void UpdateStudent(Student student)
        {
            _studentRepository.UpdateStudent(student);
=======
        public List<Student> GetAllStudentsByUserId(Guid userId)
        {
            return _studentRepository.GetAllStudentsByUserId(userId);
>>>>>>> Stashed changes
        }
    }
}
