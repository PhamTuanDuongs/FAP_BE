using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Mappings
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, ListCourseDTO>()
                .ForMember(src => src.subject, opt => opt.MapFrom(dest => dest.Subject.Code))
                .ForMember(src => src.Instructor, opt => opt.MapFrom(dest => dest.InstructorNavigation.InstructorCode))
                .ForMember(src => src.Room, opt => opt.MapFrom(dest => dest.Room))
                .ForMember(src => src.StartDate, opt => opt.MapFrom(dest => dest.StartDate.ToString("dd/MM/yyyy")))
                .ForMember(src => src.EndDate, opt => opt.MapFrom(dest => dest.EndDate.ToString("dd/MM/yyyy")))
                .ForMember(src => src.ManageSlot, opt => opt.MapFrom(dest => dest.Subject.ManageSlot));
        }
    }
}
