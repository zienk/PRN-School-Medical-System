using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Repositories.Implementations;
using Services.Interfaces;

namespace Services.Implementations
{
    public class StudentService : IStudentService
    {
        private StudentRepository _studentRepository;


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
    }
}
