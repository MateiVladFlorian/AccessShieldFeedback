#pragma warning disable
namespace AccessShieldFeedback.Models
{
    public class FeedbackPageModel
    {
        public FeedbackViewModel[] feedbackViewModels { get; set; }
        public int totalPages { get; set; }
    }
}
