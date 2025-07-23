
using DataAccessLayer;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {

        private readonly PrnEduHealthContext _context;

        public StudentRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public Student? GetStudentActiveById(int studentId)
        {
            return _context.Students
                .Include(s => s.Parent)
                .Include(s => s.Gender)
                .FirstOrDefault(s => s.StudentId == studentId && 
                                     s.IsActive == true);
        }

        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void DeleteStudent(int studentId)
        {
            var student = _context.Students.Find(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            _context.SaveChanges();
        }

        public List<Student> GetAllStudents()
        {
            return _context.Students
                .Include(u => u.Parent)
                .Include(g => g.Gender)
                .Include(s => s.HealthRecord)
                .AsNoTracking()
                .Where(s => s.IsActive == true)
                .ToList();
        }

        public List<Student> SearchStudents(string searchTerm)
        {
            return _context.Students
                .Include(s => s.Parent)
                .Include(s => s.Gender)
                .Where(s => (s.FullName.Contains(searchTerm) || 
                             s.StudentId.ToString().Contains(searchTerm)) && 
                             s.IsActive == true)
                .ToList();
        } // TODO: Xem xét thêm phân trang cho tập kết quả lớn

        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        //Thien
        public List<Student> GetAllStudentsByUserId(Guid userId)
        {
            return _context.Students
                .Include(s => s.Parent)
                .Include(s => s.Gender)
                .Where(s => s.Parent.UserId == userId && 
                            s.IsActive == true)
                .ToList();
        }

        public bool SoftDeleteStudent(int studentId)
        {
            var student = _context.Students.Find(studentId);
            if (student == null || !student.IsActive.GetValueOrDefault(true)) return false;

            student.IsActive = false;
            _context.Students.Update(student);

            //// Cascade to HealthRecord (1-1)
            //var healthRecord = _context.HealthRecords.FirstOrDefault(hr => hr.StudentId == studentId);
            //if (healthRecord != null)
            //{
            //    healthRecord.IsActive = false;
            //    _context.HealthRecords.Update(healthRecord);
            //}

            //// Cascade to Incidents (1-many)
            //var incidents = _context.Incidents.Where(i => i.StudentId == studentId).ToList();
            //foreach (var incident in incidents)
            //{
            //    incident.IsActive = false;
            //    _context.Incidents.Update(incident);
            //}

            //// Cascade to HealthCheckupResults (1-many)
            //// TODO: Tối ưu hóa truy vấn này cho tập dữ liệu lớn - xem xét cập nhật hàng loạt
            //var checkupResults = _context.HealthCheckupResults.Where(cr => cr.StudentId == studentId).ToList();
            //foreach (var result in checkupResults)
            //{
            //    result.IsActive = false;
            //    _context.HealthCheckupResults.Update(result);
            //}

            //// Cascade to VaccinationRecords (1-many)
            //// TODO: Thêm chỉ mục trên StudentId để cải thiện hiệu suất
            //var vaccRecords = _context.VaccinationRecords.Where(vr => vr.StudentId == studentId).ToList();
            //foreach (var record in vaccRecords)
            //{
            //    record.IsActive = false;
            //    _context.VaccinationRecords.Update(record);
            //}

            return _context.SaveChanges() > 0;
        }
    }
}
