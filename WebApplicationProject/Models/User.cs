namespace WebApplicationProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string SName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public List<EventParticipation> MyEvents { get; set; } = new List<EventParticipation>();
    }
}
