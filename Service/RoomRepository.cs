using FAP_BE.DataAccess;
using FAP_BE.Models;
using FAP_BE.Repository;

namespace FAP_BE.Service
{
    public class RoomRepository : IRoomRepository
    {
        List<Room> IRoomRepository.GetRooms() => RoomManagement.Instance.GetRooms();
    }
}
