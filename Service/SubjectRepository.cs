using FAP_BE.DataAccess;
using FAP_BE.DTOs;
using FAP_BE.Models;
using FAP_BE.Repository;

namespace FAP_BE.Service
{
    public class SubjectRepository : ISubjectRepository
    {
        public bool AddNewSubject(SubjectDTO subject) => SubjectManagement.Instance.AddNewSubject(subject);

        public bool DeleteSubject(int id) => SubjectManagement.Instance.DeleteSubject(id);

        public List<Subject> GetAllSubjects() => SubjectManagement.Instance.GetAllSubjects();

        public Subject GetSubjectByCode(string code) => SubjectManagement.Instance.GetSubjectByCode(code);

        public Subject GetSubjectById(int id) => SubjectManagement.Instance.GetSubjectById(id);

        public bool UpdateSubject(int id, SubjectDTO subject) => SubjectManagement.Instance.UpdateSubject(id,subject);
    }
}
