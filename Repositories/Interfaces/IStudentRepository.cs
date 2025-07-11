
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
        List<Student> GetAllStudents();
        List<Student> SearchStudents(string searchTerm);
        void AddStudent(Student student); // Hàm này nên trả về object Student
        void UpdateStudent(Student student); // Hàm này nên trả về object Student
        void DeleteStudent(Guid studentId); // Hàm này nên trả về object Student

        //Thien
        List<Student> GetAllStudentsByUserId(Guid userId);

    }
}
