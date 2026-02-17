namespace WebApplicationProject.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int stars { get; set; }
        public string reviewtitle { get; set; }
        public string reviewbody { get; set; }
        public int UserId { get; set; }

        public User Author { get; set; }
    }
    public class ProfilePageViewModel
    {
        public User UserInfo { get; set; }
        public List<Event> HostedEvents { get; set; }
        public List<Event> JoinedEvents { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
