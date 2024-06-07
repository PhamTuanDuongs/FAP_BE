namespace FAP_BE.DTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int ManageSlot { get; set; }
    }
}
