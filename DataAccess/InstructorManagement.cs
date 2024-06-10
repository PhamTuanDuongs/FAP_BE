using FAP_BE.DTOs;
using FAP_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography;

namespace FAP_BE.DataAccess
{
    public class InstructorManagement
    {
        private static FAP_PRN231Context _context;
        private static InstructorManagement instance = null;
        private static readonly object _locker = new object();

        public static InstructorManagement Instance
        {
            get
            {
                lock (_locker)
                {
                    if (instance == null)
                    {
                        instance = new InstructorManagement();
                        _context = new FAP_PRN231Context();
                    }
                    return instance;
                }
            }
        }

        public List<Instructor> GetAllInstructors()
        {
            try
            {
                var list = _context.Instructors.
                    Include(md => md.MetaData).
                    ThenInclude(a => a.Account).
                    ThenInclude(r => r.Role).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Instructor GetInstructorById(int id)
        {
            try
            {
                var instructor = _context.Instructors
                    .Include(s => s.MetaData)
                    .ThenInclude(md => md.Account)
                    .ThenInclude(a => a.Role)
                    .FirstOrDefault(s => s.Id == id);

                return instructor;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Instructor GetInstructorByCode(string code)
        {
            try
            {
                var instructor = _context.Instructors
                    .Include(s => s.MetaData)
                    .ThenInclude(md => md.Account)
                    .ThenInclude(a => a.Role)
                    .FirstOrDefault(s => s.InstructorCode.Equals(code));

                return instructor;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool AddNewInstructor(CreateNewInstructorDTO newInstructorDTO)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Metadata metadata = new Metadata();
                    metadata.Name = newInstructorDTO.Name;
                    metadata.Address = newInstructorDTO.Address;
                    metadata.Dob = newInstructorDTO.Dob;
                    metadata.Email = newInstructorDTO.Email;
                    metadata.Image = newInstructorDTO.Image;
                    _context.MetaData.Add(metadata);
                    _context.SaveChanges();

                    Account account = new Account();
                    account.Username = newInstructorDTO.Username;
                    account.Password = newInstructorDTO.Password;
                    account.RoleId = newInstructorDTO.RoleId;
                    account.MetaDataId = metadata.MetaDataId;
                    _context.Accounts.Add(account);
                    _context.SaveChanges();

                    Instructor instructor = new Instructor();
                    instructor.InstructorCode = newInstructorDTO.InstructorCode;
                    instructor.MetaDataId = metadata.MetaDataId;
                    _context.Instructors.Add(instructor);
                    _context.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool UpdateInstructor(int id, CreateNewInstructorDTO newInstructorDTO)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingInstructor = _context.Instructors.FirstOrDefault(x => x.Id == id);

                    if (existingInstructor == null) return false;

                    var existingMetaData = _context.MetaData.FirstOrDefault(x => x.MetaDataId == existingInstructor.MetaDataId);
                    var existingAccount = _context.Accounts.FirstOrDefault(x => x.MetaDataId == existingInstructor.MetaDataId);

                    existingInstructor.InstructorCode = existingInstructor.InstructorCode;

                    existingMetaData.Name = newInstructorDTO.Name;
                    existingMetaData.Dob = newInstructorDTO.Dob;
                    existingMetaData.Address = newInstructorDTO.Address;
                    existingMetaData.Image = newInstructorDTO.Image;

                    existingAccount.Password = newInstructorDTO.Password;

                    _context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public bool DeleteInstructor(int id)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingInstructor = _context.Instructors.FirstOrDefault(i => i.Id == id);
                    if (existingInstructor == null) return false;

                    int metaid = existingInstructor.MetaDataId;

                    var schedules = _context.Schedules.Where(s => s.InstructorId == id);
                    List<Attendance> attendances = new List<Attendance>();

                    foreach (var schedule in schedules)
                    {
                        attendances.Add(_context.Attendances.FirstOrDefault(a => a.ScheduleId == schedule.Id));
                    }

                    var courses = _context.Courses.Where(c => c.InstructorId == id);
                    List<StudentCourse> studentCourses = new List<StudentCourse>();

                    foreach (var course in courses)
                    {
                        studentCourses.Add(_context.StudentCourse.FirstOrDefault(sc => sc.CourseId == course.Id));
                    }

                    var existingMetaData = _context.MetaData.FirstOrDefault(x => x.MetaDataId == metaid);
                    var existingAccount = _context.Accounts.FirstOrDefault(x => x.MetaDataId == metaid);

                    _context.Attendances.RemoveRange(attendances);
                    _context.Schedules.RemoveRange(schedules);
                    _context.SaveChanges();

                    _context.StudentCourse.RemoveRange(studentCourses);
                    _context.Courses.RemoveRange(courses);
                    _context.SaveChanges();

                    _context.Instructors.Remove(existingInstructor);
                    _context.Accounts.Remove(existingAccount);
                    _context.MetaData.Remove(existingMetaData);
                    _context.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }


    }
}
