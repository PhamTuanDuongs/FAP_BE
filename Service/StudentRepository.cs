using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using FAP_BE.Validations;

namespace FAP_BE.Service
{
    public class StudentRepository : IStudentRepository
    {
        public string AddNewStudent(CreateNewStudentDTO createNewStudentDTO)
        {
            try
            {
                string result = StudentValidation.Instance.CheckStudentInDb(createNewStudentDTO);
                if (result.Equals(""))
                {
                    bool check = StudentManagement.Instance.AddNewStudent(createNewStudentDTO);
                    if (check) return "Add a new student successfully";
                }
                return result;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteStudent(int id) => StudentManagement.Instance.DeleteStudent(id);

        public List<Student> GetAllStudents() => StudentManagement.Instance.GetAllStudents();

        public Student GetStudentById(int id) => StudentManagement.Instance.GetStudentById(id);

        public Student GetStudentByRoleNumber(string roleNumber) => StudentManagement.Instance.GetStudentByRoleNumber(roleNumber);

        public bool UpdateStudent(int id, CreateNewStudentDTO student) => StudentManagement.Instance.UpdateStudent(id, student);

    }
}
