
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

        public void DeleteStudent(int studentId)
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
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Học sinh bị null");

            var existing = _studentRepository.GetStudentActiveById(student.StudentId);
            if (existing == null)
                throw new InvalidOperationException("Không tìm thấy học sinh để cập nhật.");

            existing.FullName = student.FullName;
            existing.DateOfBirth = student.DateOfBirth;
            existing.GenderId = student.GenderId;
            existing.ParentId = student.ParentId;
            existing.Class = student.Class;
            existing.IsActive = student.IsActive;

            _studentRepository.UpdateStudent(existing);
        }


        //Thien
        public List<Student> GetAllStudentsByUserId(Guid userId)
        {
            return _studentRepository.GetAllStudentsByUserId(userId);
        }

        public bool SoftDeleteStudent(int studentId)
        {
            // TODO: Thêm logging cho audit trail
            return _studentRepository.SoftDeleteStudent(studentId);
        } // Lưu ý: Các truy vấn đã được lọc ở tầng repository
    }
}
