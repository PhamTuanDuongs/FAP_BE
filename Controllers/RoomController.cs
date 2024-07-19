using AutoMapper;
using FAP_BE.DTOs;
using FAP_BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FAP_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        IMapper _mapper;

        public RoomController(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllRooms")]
        public IActionResult Get()
        {
            var listRooms = _mapper.Map<List<RoomDTO>>(_roomRepository.GetRooms());
            return Ok(listRooms);
        }

    }
}
