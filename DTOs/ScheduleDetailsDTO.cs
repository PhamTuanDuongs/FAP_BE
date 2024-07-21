namespace FAP_BE.DTOs
{
    public class ScheduleDetailsDTO
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Slot { get; set; } 
        public string CourseCode { get; set; } = null!;
        public string RoomName { get; set; } = null!;
        public string InstructorName { get; set; } = null!;
        public string TimeSlot { get; set; }
        public bool Status { get; set; } = false;
    }
}
