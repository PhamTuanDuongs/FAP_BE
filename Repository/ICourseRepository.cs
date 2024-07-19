using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Repository
{
    public interface ICourseRepository
    {
        public string AddNewCourse(CreateNewCourseDTO courseDTO);

        public List<Course> GetCourses();

        public bool DeleteCourse(int courseId);

        public void UpdateCourse(CourseDTO course);

        public List<Course> GetCourseByInstructorId(int instructorId);

    }
}
