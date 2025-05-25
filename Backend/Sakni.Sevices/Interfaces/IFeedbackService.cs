using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.DA.DTOs;

namespace Sakni.Sevices.Interfaces
{
    public interface IFeedbackService
    {
        Task<FeedbackDto> GetFeedbackByIdAsync(Guid id);
        Task<IEnumerable<FeedbackDto>> GetFeedbacksByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null);
        Task<IEnumerable<FeedbackDto>> GetFeedbacksByUserIdAsync(string userId, int? skip = null, int? take = null);
        Task<FeedbackDto> CreateFeedbackAsync(CreateFeedbackDto request);
        Task<FeedbackDto> UpdateFeedbackAsync(Guid id, UpdateFeedbackDto request);
        Task<bool> DeleteFeedbackAsync(Guid id);
        Task<FeedbackDto> GetFeedbackWithDetailsAsync(Guid feedbackId);
        Task<bool> UpdateFeedbackRatingAsync(Guid feedbackId, int newRating);
        Task<int> GetTotalFeedbacksCountAsync();
        Task<decimal> GetAverageRatingByApartmentIdAsync(Guid apartmentId);
        Task<IEnumerable<FeedbackDto>> GetFeedbacksByRatingRangeAsync(int minRating, int maxRating, int? skip = null, int? take = null);
        Task<bool> FeedbackExistsByUserAndApartmentAsync(string userId, Guid apartmentId);
        Task<FeedbackDto> GetFeedbackByUserAndApartmentAsync(string userId, Guid apartmentId);
    }
}
