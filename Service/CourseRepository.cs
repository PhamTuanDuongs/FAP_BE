using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using FAP_BE.Validations;

namespace FAP_BE.Service
{
    public class CourseRepository : ICourseRepository
    {
        public string AddNewCourse(CreateNewCourseDTO courseDTO)
        {
            try
            {
                string result = CourseValidation.Instance.CheckCourseInDb(courseDTO);
                if (result == "No case matched")
                {
                    bool check = CourseManagement.Instance.AddNewCourse(courseDTO);
                    if (check) return "Add a new course successfully";
                    return "Add a new course fail";
                }

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
