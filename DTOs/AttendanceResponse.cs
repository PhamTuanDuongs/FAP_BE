namespace FAP_BE.DTOs
{
   
    public class AttendanceResponse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ScheduleId { get; set; }
        public DateTime? DateAttended { get; set; }
        public int? Status { get; set; }
        public string? Comment { get; set; }
        public string RoleNumber { get; set; }
    }
}
