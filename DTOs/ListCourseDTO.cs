namespace FAP_BE.DTOs
{
    public class ListCourseDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;

        public string Instructor {  get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string subject { get; set; }

        public string Room { get; set; }

        public int ManageSlot { get; set; }


    }
}
