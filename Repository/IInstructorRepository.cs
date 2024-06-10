using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Repository
{
    public interface IInstructorRepository
    {
        public List<Instructor> GetAllInstructor();
        public Instructor GetInstructorById(int id);
        public Instructor GetInstructorByCode(string code);
        public string AddNewInstructor(CreateNewInstructorDTO createNewStudentDTO);
        public bool UpdateInstructor(int id, CreateNewInstructorDTO createNewInstructorDTO);
        public bool DeleteInstuctor(int id);
    }
}
