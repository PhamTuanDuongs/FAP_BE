using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.DataAccess
{
    public class CourseManagement
    {
        private static FAP_PRN231Context _context;
        private static CourseManagement instance = null;
        private static readonly object _locker = new object();

        public static CourseManagement Instance
        {
            get
            {
                lock (_locker)
                {
                    if (instance == null)
                    {
                        instance = new CourseManagement();
                        _context = new FAP_PRN231Context();
                    }
                    return instance;
                }
            }
        }


        public bool AddNewCourse(CreateNewCourseDTO courseDTO)
        {
            try
            {
                Course course = new Course();
                course.Code = courseDTO.Code;
                course.SubjectId = courseDTO.SubjectId;
                course.StartDate = courseDTO.StartDate;
                course.EndDate = courseDTO.EndDate;
                course.InstructorId = courseDTO.instructorId;
                course.TimeSlot = courseDTO.TimeSlot;
                course.Room = courseDTO.Room;
                _context.Courses.Add(course);
                _context.SaveChanges();
                List<StudentCourse> studentCourses = new List<StudentCourse>();
                foreach (var s in courseDTO.Students)
                {
                    StudentCourse studentCourse = new StudentCourse();
                    studentCourse.StudentId = s.Id;
                    studentCourse.CourseId = course.Id;
                    studentCourses.Add(studentCourse);
                }
                AddStudentCourse(studentCourses);


                switch (course.TimeSlot[1])
                {
                    case '2':
                        DateTime startDateForMonday = GetNextOrSameDay(course.StartDate, DayOfWeek.Monday);
                        GenerateScheduleAndAttendanceForSlot1or3(startDateForMonday, course);
                        break;
                    case '3':
                        DateTime startDateForTuseDay = GetNextOrSameDay(course.StartDate, DayOfWeek.Tuesday);
                        GenerateScheduleAndAttendanceForSlot1or3(startDateForTuseDay, course);
                        break;
                    case '4':
                        DateTime startDateForWed = GetNextOrSameDay(course.StartDate, DayOfWeek.Wednesday);
                        GenerateScheduleAndAttendanceForSlot1or3(startDateForWed, course);
                        break;
                    case '5':
                        DateTime startDateForThus = GetNextOrSameDay(course.StartDate, DayOfWeek.Thursday);
                        GenerateScheduleAndAttendanceForSlot1or3(startDateForThus, course);
                        break;
                    case '6':
                        DateTime startDateForFri = GetNextOrSameDay(course.StartDate, DayOfWeek.Friday);
                        GenerateScheduleAndAttendanceForSlot1or3(startDateForFri, course);
                        break;
                }

                switch (course.TimeSlot[2])
                {
                    case '2':
                        DateTime startDateForMonday = GetNextOrSameDay(course.StartDate, DayOfWeek.Monday);
                        GenerateScheduleAndAttendanceForSlot2or4(startDateForMonday, course);
                        break;
                    case '3':
                        DateTime startDateForTuseDay = GetNextOrSameDay(course.StartDate, DayOfWeek.Tuesday);
                        GenerateScheduleAndAttendanceForSlot2or4(startDateForTuseDay, course);
                        break;
                    case '4':
                        DateTime startDateForWed = GetNextOrSameDay(course.StartDate, DayOfWeek.Wednesday);
                        GenerateScheduleAndAttendanceForSlot2or4(startDateForWed, course);
                        break;
                    case '5':
                        DateTime startDateForThus = GetNextOrSameDay(course.StartDate, DayOfWeek.Thursday);
                        GenerateScheduleAndAttendanceForSlot2or4(startDateForThus, course);
                        break;
                    case '6':
                        DateTime startDateForFri = GetNextOrSameDay(course.StartDate, DayOfWeek.Friday);
                        GenerateScheduleAndAttendanceForSlot2or4(startDateForFri, course);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private DateTime GetNextOrSameDay(DateTime givenDate, DayOfWeek targetDay)
        {
            int givenWeekday = (int)givenDate.DayOfWeek;
            int targetWeekday = (int)targetDay;

            if (givenWeekday == targetWeekday) return givenDate;
            int daysUntilTargetDay = (targetWeekday - givenWeekday + 7) % 7;

            DateTime nextTargetDayDate = givenDate.AddDays(daysUntilTargetDay);

            return nextTargetDayDate;
        }

        private void GenerateScheduleAndAttendanceForSlot1or3(DateTime startDate, Course course)
        {
            while (startDate <= course.EndDate)
            {
                Schedule s = new Schedule();
                s.Date = startDate;
                s.InstructorId = course.InstructorId;
                s.CourseId = course.Id;
                s.Room = course.Room;
                s.Slot = course.TimeSlot[0] == 'A' ? 1 : 3;
                _context.Schedules.Add(s);
                _context.SaveChanges();
                List<Attendance> attendances = new List<Attendance>();
                foreach (StudentCourse st in course.StudentCourses)
                {
                    Attendance a = new Attendance();
                    a.StudentId = st.StudentId;
                    a.ScheduleId = s.Id;
                    a.Status = 0;
                    attendances.Add(a);
                }

                _context.Attendances.AddRange(attendances);
                _context.SaveChanges();

                startDate = startDate.AddDays(7);
            }
        }

        private void GenerateScheduleAndAttendanceForSlot2or4(DateTime startDate, Course course)
        {
            while (startDate <= course.EndDate)
            {
                Schedule s = new Schedule();
                s.Date = startDate;
                s.InstructorId = course.InstructorId;
                s.CourseId = course.Id;
                s.Room = course.Room;
                s.Slot = course.TimeSlot[0] == 'A' ? 2 : 4;
                _context.Schedules.Add(s);
                _context.SaveChanges();
                List<Attendance> attendances = new List<Attendance>();
                foreach (StudentCourse st in course.StudentCourses)
                {
                    Attendance a = new Attendance();
                    a.StudentId = st.StudentId;
                    a.ScheduleId = s.Id;
                    a.Status = 0;
                    attendances.Add(a);
                }

                _context.Attendances.AddRange(attendances);
                _context.SaveChanges();
                startDate = startDate.AddDays(7);

            }
        }

        private void AddStudentCourse(List<StudentCourse> studentCourse)
        {
            _context.StudentCourse.AddRange(studentCourse);
            _context.SaveChanges();
        }
    }
}
