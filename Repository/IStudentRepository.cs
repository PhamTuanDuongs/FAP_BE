using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Repository
{
    public interface IStudentRepository
    {
        public List<Student> GetAllStudents();
        public Student GetStudentById(int id);

        public Student GetStudentByRoleNumber(string roleNumber);

        public string AddNewStudent(CreateNewStudentDTO createNewStudentDTO);
        public bool UpdateStudent(int id, CreateNewStudentDTO student);
        public bool DeleteStudent(int id);

    }
}
