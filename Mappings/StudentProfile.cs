using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentInfoDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MetaData.Name))
                .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => src.MetaData.Dob.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.MetaData.Address))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.MetaData.Email))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.MetaData.Image))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.MetaData.Account.Username))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.MetaData.Account.Role.Name));
        }
    }
}
