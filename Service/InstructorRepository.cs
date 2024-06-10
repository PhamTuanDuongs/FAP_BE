using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;
using FAP_BE.Validations;

namespace FAP_BE.Service
{
    public class InstructorRepository : IInstructorRepository
    {
        public string AddNewInstructor(CreateNewInstructorDTO createNewStudentDTO)
        {
            try
            {
                string result = InstructorValidation.Instance.CheckInstructorInDb(createNewStudentDTO);
                if (result.Equals(""))
                {
                    bool check = InstructorManagement.Instance.AddNewInstructor(createNewStudentDTO);
                    if (check) return "Add a new instructor successfully";
                }
                return result;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteInstuctor(int id) => InstructorManagement.Instance.DeleteInstructor(id);

        public List<Instructor> GetAllInstructor() => InstructorManagement.Instance.GetAllInstructors();

        public Instructor GetInstructorByCode(string code) => InstructorManagement.Instance.GetInstructorByCode(code);

        public Instructor GetInstructorById(int id) => InstructorManagement.Instance.GetInstructorById(id);

        public bool UpdateInstructor(int id, CreateNewInstructorDTO createNewInstructorDTO) => InstructorManagement.Instance.UpdateInstructor(id, createNewInstructorDTO);
    }
}
