using FAP_BE.DTOs;
using FAP_BE.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net.WebSockets;
using System.Security.Cryptography;

namespace FAP_BE.DataAccess
{
    public class SubjectManagement
    {
        private static FAP_PRN231Context _context;
        private static SubjectManagement instance = null;
        private static readonly object _locker = new object();

        public static SubjectManagement Instance
        {
            get
            {
                lock (_locker)
                {
                    if (instance == null)
                    {
                        instance = new SubjectManagement();
                        _context = new FAP_PRN231Context();
                    }
                    return instance;
                }
            }
        }

        public List<Subject> GetAllSubjects()
        {
            try
            {
                var list = _context.Subjects.ToList();
                return list;
            }
            catch (Exception ex) {
                return null;
            }

        }

        public Subject GetSubjectById(int id)
        {
            try
            {
                var subject = _context.Subjects.FirstOrDefault(x => x.Id == id);
                return subject;
            }catch (Exception ex)
            {
                return null;
            }
        }

        public Subject GetSubjectByCode(string code)
        {
            try
            {
                var subject = _context.Subjects.FirstOrDefault(x => x.Code.Equals(code));
                return subject;
            }catch(Exception ex)
            {
                return null;
            }
        }

        public bool AddNewSubject(SubjectDTO subject)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Subject subject1 = new Subject();
                    subject1.Code = subject.Code;
                    subject1.Name = subject.Name;
                    subject1.ManageSlot = subject.ManageSlot;
                    _context.Subjects.Add(subject1);
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

        public bool UpdateSubject(int sid, SubjectDTO subject)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingSubject = _context.Subjects.FirstOrDefault(x => x.Id == sid);

                    if (existingSubject == null) return false;

                    existingSubject.Code = subject.Code;
                    existingSubject.Name = subject.Name;
                    existingSubject.ManageSlot = subject.ManageSlot;
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

        public bool DeleteSubject(int sid)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingSubject = _context.Subjects.FirstOrDefault(x => x.Id == sid);

                    if (existingSubject == null) return false;

                    var courses = _context.Courses.Where(c => c.Id == sid).ToList();

                    List<Schedule> schedules = new List<Schedule>();
                    List<Attendance> attendances = new List<Attendance>();
                    List<StudentCourse> studentCourses = new List<StudentCourse>();

                    foreach (var course in courses)
                    {
                        studentCourses.Add(_context.StudentCourse.FirstOrDefault(sc => sc.CourseId == course.Id));
                        schedules.Add(_context.Schedules.FirstOrDefault(s => s.CourseId == course.Id));
                    }

                    foreach (var schedule in schedules)
                    {
                        attendances.Add(_context.Attendances.FirstOrDefault(a => a.ScheduleId == schedule.Id));
                    }

                    _context.Attendances.RemoveRange(attendances);
                    _context.SaveChanges();
                    _context.Schedules.RemoveRange(schedules);
                    _context.SaveChanges();

                    _context.StudentCourse.RemoveRange(studentCourses);
                    _context.SaveChanges();
                    _context.Courses.RemoveRange(courses);
                    _context.SaveChanges();

                    _context.Subjects.Remove(existingSubject);
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
