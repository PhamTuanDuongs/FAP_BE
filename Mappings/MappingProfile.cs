using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Attendance, AttendanceDTO>()
                .ForMember(src => src.ScheduleDTONav, opt => opt.MapFrom(s =>
                new ScheduleDTO
                {
                    Id = s.ScheduleId,
                    InstructorCode = s.Schedule.Instructor.InstructorCode,
                    CourseId = s.Schedule.CourseId,
                    Slot = s.Schedule.Slot,
                    Date = s.Schedule.Date.ToString("dd/MM"),
                    Room = s.Schedule.Room,
                    Course = new CourseDTO  
                    {
                        Id = s.Schedule.Course.Id,
                        Code = s.Schedule.Course.Code,
                        SubjectId = s.Schedule.Course.SubjectId,
                        StartDate = s.Schedule.Course.StartDate,
                        EndDate = s.Schedule.Course.EndDate,
                        Subject = new SubjectDTO
                        {
                            Id = s.Schedule.Course.Subject.Id,
                            Code = s.Schedule.Course.Subject.Code,
                            Name = s.Schedule.Course.Subject.Name,
                            ManageSlot = s.Schedule.Course.Subject.ManageSlot,
                        }
                    }
                }
                ));


            CreateMap<Schedule, ScheduleDTO>().
                ForMember(src => src.Date, opt => opt.MapFrom(s => s.Date.ToString("dd/MM"))).
                ForMember(src => src.InstructorCode, opt => opt.MapFrom(s => s.Instructor.InstructorCode)).
                ForMember(src => src.Course, opt => opt.MapFrom(s => new CourseDTO
                {
                    Id = s.Course.Id,
                    Code = s.Course.Code,
                    SubjectId = s.Course.SubjectId,
                    StartDate = s.Course.StartDate,
                    EndDate = s.Course.EndDate,
                    Subject = new SubjectDTO
                    {
                        Code = s.Course.Subject.Code,
                        Name = s.Course.Subject.Name,
                        ManageSlot = s.Course.Subject.ManageSlot,
                    }
                }));

            CreateMap<Account, AccountInfoDTO>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
