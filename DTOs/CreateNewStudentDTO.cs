﻿namespace FAP_BE.DTOs
{
    public class CreateNewStudentDTO
    {
        public CreateNewStudentDTO() { }

        public string RoleNumber { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Email { get; set; } = null!;
        public string? Image { get; set; }
        public int RoleId { get; set; }
    }
}
