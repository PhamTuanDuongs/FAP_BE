using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Repository
{
    public interface ISubjectRepository
    {
        public List<Subject> GetAllSubjects();
        public Subject GetSubjectById(int id);
        public Subject GetSubjectByCode(string code);
        public bool AddNewSubject(SubjectDTO course);
        public bool UpdateSubject(int id,SubjectDTO course);
        public bool DeleteSubject(int id);
    }
}
