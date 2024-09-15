namespace IDSProject.core.Dtos
{
    public class EventMemberDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; } = null!;
    }
}