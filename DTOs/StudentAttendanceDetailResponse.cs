namespace FAP_BE.DTOs
{
    public class StudentAttendanceDetailResponse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string RoleNumber { get; set; }
        public int ScheduleId { get; set; }
        public string? DateAttended { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string CourseName { get; set; }
        public string SubjectName { get; set; }
        public string CourseCode { get; set; }
        public string RoomName { get; set; }
        public string TimeSlot { get; set; }
        public int Slot { get; set; }
        public string InstructorName { get; set; }
    }
}
