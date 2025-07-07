using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Entities;

namespace Repositories.Interfaces
{
    public interface IStudentRepository
    {
        List<Student> GetAllStudents();
        List<Student> SearchStudents(string searchTerm);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Guid studentId);

    }
}
