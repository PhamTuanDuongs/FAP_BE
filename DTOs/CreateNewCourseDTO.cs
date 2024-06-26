﻿using FAP_BE.Models;

namespace FAP_BE.DTOs
{
    public class CreateNewCourseDTO
    {
        public CreateNewCourseDTO() { }
        public string Code { get; set; }
        public int SubjectId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int instructorId { get; set; }
        public string TimeSlot { get; set; } = null!;
        public string Room { get; set; }
        public List<StudentDTO> Students { get; set; }
    }
}
