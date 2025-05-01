#pragma warning disable
namespace AccessShieldFeedback.Models
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
