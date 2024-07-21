namespace FAP_BE.DTOs
{
    public class Statistics_Attendance
    {
        public string CourseName { get; set; }

        public string RollNumber {  get; set; }

        public string StudentName { get; set; }

        public List<AttendanceDTO> Attendances { get; set; }

        public int  Summary { get; set; }

        public int  Percentage { get; set; }
    }
}
