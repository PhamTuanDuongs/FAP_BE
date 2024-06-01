using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Repository;

namespace FAP_BE.Service
{
    public class CourseRepository : ICourseRepository
    {
        public bool AddNewCourse(CreateNewCourseDTO courseDTO) => CourseManagement.Instance.AddNewCourse(courseDTO);
    }
}
