using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Validations
{
    public class StudentValidation
    {
        private static FAP_PRN231Context _context;
        private static readonly object _contextLock = new object();
        private static StudentValidation instance;

        public static StudentValidation Instance
        {
            get
            {
                if (instance == null || _context == null)
                {
                    instance = new StudentValidation();
                    _context = new FAP_PRN231Context();
                }
                return instance;
            }
        }

        public string CheckStudentInDb(CreateNewStudentDTO createNewInstructorDTO)
        {
            try
            {
                var instructors = _context.Students.ToList();
                var metadatas = _context.MetaData.ToList();
                var accounts = _context.Accounts.ToList();

                string result = "";
                foreach (var instructor in instructors)
                {
                    bool dupplicateCode = instructor.RoleNumber.ToUpper().Equals(createNewInstructorDTO.RoleNumber.ToUpper());
                    if (dupplicateCode)
                    {
                        result += "Dupplicate student rolenumber\n";
                        break;
                    }
                }

                foreach (var metadata in metadatas)
                {
                    bool dupplicateEmail = metadata.Email.ToUpper().Equals(createNewInstructorDTO.Email.ToUpper());
                    if (dupplicateEmail)
                    {
                        result += "Dupplicate email \n";
                        break;
                    }
                }

                foreach (var account in accounts)
                {
                    bool dupplicateUsername = account.Username.ToUpper().Equals(createNewInstructorDTO.Username.ToUpper());
                    if (dupplicateUsername)
                    {
                        result += "Dupplicate username \n";
                    }
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
