<<<<<<< Updated upstream
﻿using System;
=======
﻿using DataAccessLayer;
using Repositories.Interfaces;
using System;
>>>>>>> Stashed changes
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Entities;
<<<<<<< Updated upstream
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

=======

using Microsoft.EntityFrameworkCore;
>>>>>>> Stashed changes
namespace Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
<<<<<<< Updated upstream
        PrnEduHealthContext _context;

=======
        private readonly PrnEduHealthContext _context;
>>>>>>> Stashed changes
        public StudentRepository()
        {
            _context = new PrnEduHealthContext();
        }
<<<<<<< Updated upstream
        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void DeleteStudent(Guid studentId)
        {
            _context.Students.Remove(_context.Students.Find(studentId));
            _context.SaveChanges();
        }

        public List<Student> GetAllStudents()
        {
            return _context.Students.Include(u => u.Parent).Include(g=>g.Gender).ToList();
        }

        public List<Student> SearchStudents(string searchTerm)
        {
            return _context.Students
                .Where(s => s.FullName.Contains(searchTerm) || s.StudentId.ToString().Contains(searchTerm))
                .ToList();
        }

        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }
=======
        public List<Student> GetAllStudentsByUserId(Guid userId)
        {
            return _context.Students
                .Include(s => s.Parent)
                .Where(s => s.Parent.UserId == userId && s.IsActive == true)
                .ToList();
        }
>>>>>>> Stashed changes
    }
}
