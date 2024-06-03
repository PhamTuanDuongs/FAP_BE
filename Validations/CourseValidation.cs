using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Validations
{
    public class CourseValidation
    {
        private static FAP_PRN231Context _context;
        private static readonly object _contextLock = new object();
        private static CourseValidation instance;

        public static CourseValidation Instance
        {
            get
            {
                if (instance == null || _context == null)
                {
                    instance = new CourseValidation();
                    _context = new FAP_PRN231Context();
                }
                return instance;
            }
        }


        public string CheckCourseInDb(CreateNewCourseDTO courseDTO)
        {
            try
            {
                var courses = _context.Courses.ToList();

                Course matchedCourse = null;
                string matchedCase = "No case matched";

                foreach (var course in courses)
                {
                    bool case1 = course.Code.ToUpper().Equals(courseDTO.Code.ToUpper());
                    bool case2 = CheckTimeSlot(course.TimeSlot.Trim(), courseDTO.TimeSlot.Trim()) &&
                                 (DateTime.Parse(courseDTO.StartDate) >= course.StartDate && DateTime.Parse(courseDTO.StartDate) <= course.EndDate) &&
                                 (course.InstructorId == courseDTO.instructorId);
                    bool case3 = course.Code.Trim().Equals(courseDTO.Code.Trim()) &&
                                 course.SubjectId == courseDTO.SubjectId;
                    bool case4 = CheckTimeSlot(course.TimeSlot.Trim(), courseDTO.TimeSlot.Trim()) &&
                                 !course.Room.Trim().Equals(courseDTO.Room.Trim()) &&
                                 (DateTime.Parse(courseDTO.StartDate) >= course.StartDate && DateTime.Parse(courseDTO.StartDate) <= course.EndDate);

                    switch (true)
                    {
                        case bool _ when case1:
                            matchedCase = "Name course is already exist";
                            matchedCourse = course;
                            break;
                        case bool _ when case2:
                            matchedCase = "Instructor already is created with this TimeSlot and StartDate,EndDate";
                            matchedCourse = course;
                            break;
                        case bool _ when case3:
                            matchedCase = "This course is already exist";
                            matchedCourse = course;
                            break;
                        case bool _ when case4:
                            matchedCase = "Conflict TimeSlot, Room and StartDate, EndDate";
                            matchedCourse = course;
                            break;
                    }

                    if (matchedCourse != null)
                    {
                        break;
                    }
                }

                if (matchedCourse != null)
                {
                    return matchedCase;
                }

                return matchedCase;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private bool CheckTimeSlot(string slotIndb, string slotInCsv)
        {
            char[] charInDb = slotIndb.ToCharArray();
            char[] charInCsv = slotInCsv.ToCharArray();
            if (charInDb[0].Equals(charInCsv[0]))
            {
                if (charInDb[1].Equals(charInCsv[1]) || charInDb[2].Equals(charInCsv[2]))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
