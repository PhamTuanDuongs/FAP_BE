namespace FAP_BE.DTOs
{
    public class AccountInfoDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Username { get; set; } = null!;

        public string Role { get; set; }
    }
}
