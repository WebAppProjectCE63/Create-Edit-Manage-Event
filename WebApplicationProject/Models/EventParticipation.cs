namespace WebApplicationProject.Models
{
    public class EventParticipation
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinedAt { get; set; }
        public ParticipationStatus Status { get; set; }

    }
}
