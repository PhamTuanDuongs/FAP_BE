using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Models;

namespace FAP_BE.Mappings
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDTO>();

        }
    }
}
