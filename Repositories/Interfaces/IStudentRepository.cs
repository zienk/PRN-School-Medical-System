<<<<<<< Updated upstream
﻿using System;
=======
﻿using BusinessObjects.Entities;
using System;
>>>>>>> Stashed changes
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< Updated upstream
using BusinessObjects.Entities;
=======
>>>>>>> Stashed changes

namespace Repositories.Interfaces
{
    public interface IStudentRepository
    {
<<<<<<< Updated upstream
        List<Student> GetAllStudents();
        List<Student> SearchStudents(string searchTerm);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Guid studentId);

=======
        public List<Student> GetAllStudentsByUserId(Guid userId);
>>>>>>> Stashed changes
    }
}
