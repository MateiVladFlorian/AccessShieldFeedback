
#pragma warning disable

using AccessShieldFeedback.Data;
using AccessShieldFeedback.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessShieldFeedback.Services
{
    public class FeedbackService : IFeedbackService
    {
        readonly FeedbackContext feedbackContext;

        public FeedbackService(FeedbackContext feedbackContext)
        { this.feedbackContext = feedbackContext; }

        public async Task<bool> AddOrUpdateFeedbackAsync(FeedbackModel feedbackModel)
        {
            /* find an existing feedback record */
            var existingFeedback = await feedbackContext.Feedback
                .FirstOrDefaultAsync(e => e.Username.CompareTo(feedbackModel.username) == 0 
                && e.Address.CompareTo(feedbackModel.address) == 0);

            if(existingFeedback != null)
            {
                /* update existing one */
                existingFeedback.Rating = feedbackModel.rating;
                existingFeedback.Message = feedbackModel.message;
                
                existingFeedback.CreatedAt = DateTime.UtcNow;
                await feedbackContext.SaveChangesAsync();
                return true;
            }
            else
            {
                /* create a new feedback */
                existingFeedback = new Feedback
                {
                    Username = feedbackModel.username,
                    Address = feedbackModel.address,
                    Rating = feedbackModel.rating,
                    Message = feedbackModel.message,
                    CreatedAt = DateTime.UtcNow
                };

                /* update the database */
                feedbackContext.Feedback.Add(existingFeedback);
                await feedbackContext.SaveChangesAsync();
                return false;
            }
        }

        public async Task<FeedbackPageModel> GetAllFeedbacksAsync(FeedbackQueryFilter filter, int? page)
        {
            int index = (page != null && page.Value >= 1) ? page.Value - 1 : 0;
            Feedback[] feedbacks = await feedbackContext.Feedback.ToArrayAsync();

            List<Feedback> feedbacksList = new List<Feedback>();
            bool[] map = new bool[feedbacks.Length];

            for (int i = 0; i < map.Length; i++) 
                map[i] = true;

            /* filter the feedbacks provided by users */
            for (int i = 0; i < feedbacks.Length; i++)
            {
                if (filter.username != null && !feedbacks[i].Username.StartsWith(filter.username)) map[i] = false;
                if (filter.address != null && !feedbacks[i].Address.StartsWith(filter.address) && map[i]) map[i] = false;

                if (filter.rating != null && feedbacks[i].Rating != filter.rating && map[i]) map[i] = false;
                if (map[i]) feedbacksList.Add(feedbacks[i]);
            }

            int counter = feedbacksList.Count;
            int totalPages = counter / 8;

            /* add one more page and get an array of FeedbackViewModel type */
            if (counter % 8 != 0) totalPages++;
            FeedbackPageModel pageModel = new FeedbackPageModel();

            pageModel.feedbackViewModels = feedbacksList.Skip(8 * index)
                .Take(8).Select(e => new FeedbackViewModel
                {
                    Id = e.Id,
                    Username = e.Username,
                    Address = e.Address,
                    Message = e.Message,
                    Rating = e.Rating,
                    CreatedAt = e.CreatedAt
                }).ToArray();

            pageModel.totalPages = totalPages;
            return pageModel;
        }
    }
}
