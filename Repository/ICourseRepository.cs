using FAP_BE.DTOs;

namespace FAP_BE.Repository
{
    public interface ICourseRepository
    {
        public string AddNewCourse(CreateNewCourseDTO courseDTO);
    }
}
