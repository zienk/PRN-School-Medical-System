
﻿using System;

﻿using BusinessObjects.Entities;
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
    public class StudentService : IStudentService
    {

        private readonly IStudentRepository _studentRepository;

        public StudentService()
        {
            _studentRepository = new StudentRepository();
        }


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
        }


        //Thien
        public List<Student> GetAllStudentsByUserId(Guid userId)
        {
            return _studentRepository.GetAllStudentsByUserId(userId);
        }
    }
}
