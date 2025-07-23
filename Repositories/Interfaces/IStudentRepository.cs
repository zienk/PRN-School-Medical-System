
﻿using System;

﻿using BusinessObjects.Entities;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Student? GetStudentActiveById(int studentId);
        List<Student> GetAllStudents();
        List<Student> SearchStudents(string searchTerm);
        void AddStudent(Student student); // Hàm này nên trả về object Student
        void UpdateStudent(Student student); // Hàm này nên trả về object Student
        void DeleteStudent(int studentId); // Hàm này nên trả về bool

        //Thien
        List<Student> GetAllStudentsByUserId(Guid userId);

        // TODO: Implement soft delete to set IsActive=false instead of removing
        bool SoftDeleteStudent(int studentId); // Returns bool for success
    }
}
