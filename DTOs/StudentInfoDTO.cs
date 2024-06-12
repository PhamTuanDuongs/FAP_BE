namespace FAP_BE.DTOs
{
    public class StudentInfoDTO
    {
        public int Id { get; set; }
        public string RoleNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Email { get; set; } = null!;
        public string? Image { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

    }
}
