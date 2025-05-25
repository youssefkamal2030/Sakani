using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA.Interfaces
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetFeedbacksByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null);
        Task<IEnumerable<Feedback>> GetFeedbacksByUserIdAsync(string userId, int? skip = null, int? take = null);
        Task<Feedback> GetFeedbackWithDetailsAsync(Guid feedbackId);
        Task<bool> UpdateFeedbackRatingAsync(Guid feedbackId, int newRating);
        Task<int> GetTotalFeedbacksCountAsync();
        Task<decimal> GetAverageRatingByApartmentIdAsync(Guid apartmentId);
        Task<IEnumerable<Feedback>> GetFeedbacksByRatingRangeAsync(int minRating, int maxRating, int? skip = null, int? take = null);
        Task<bool> FeedbackExistsByUserAndApartmentAsync(string userId, Guid apartmentId);
        Task<Feedback> GetFeedbackByUserAndApartmentAsync(string userId, Guid apartmentId);
    }
}
