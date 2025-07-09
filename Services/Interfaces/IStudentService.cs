
﻿using System;

﻿using BusinessObjects.Entities;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessObjects.Entities;


namespace Services.Interfaces
{
    public interface IStudentService
    {

        List<Student> GetAllStudents();
        List<Student> SearchStudents(string searchTerm);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Guid studentId);



        //Thien
        public List<Student> GetAllStudentsByUserId(Guid userId);
    }
}
