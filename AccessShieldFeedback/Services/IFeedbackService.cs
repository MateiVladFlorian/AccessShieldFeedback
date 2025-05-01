using AccessShieldFeedback.Models;

namespace AccessShieldFeedback.Services
{
    public interface IFeedbackService
    {
        Task<bool> AddOrUpdateFeedbackAsync(FeedbackModel feedbackModel);
        Task<FeedbackPageModel> GetAllFeedbacksAsync(FeedbackQueryFilter filter, int? page);
    }
}
