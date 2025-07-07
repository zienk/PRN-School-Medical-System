using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Entities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        PrnEduHealthContext _context;

        public StudentRepository()
        {
            _context = new PrnEduHealthContext();
        }
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
    }
}
