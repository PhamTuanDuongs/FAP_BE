namespace FAP_BE.DTOs
{
    public class AttendanceDTO
    {

        public int StudentId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime? DateAttended { get; set; }
        public int? Status { get; set; }
        public string? Comment { get; set; }

        public  ScheduleDTO ScheduleDTONav { get; set; }
    }
}
