using FAP_BE.Models;

namespace FAP_BE.DataAccess
{
    public class RoomManagement
    {
        private static FAP_PRN231Context _context;
        private static RoomManagement instance = null;
        private static readonly object _locker = new object();

        public static RoomManagement Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomManagement();
                    _context = new FAP_PRN231Context();
                }

                return instance;
            }
        }

        public List<Room> GetRooms()
        {
            try
            {
                var listRooms = _context.Rooms.ToList();
                return listRooms;

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
