using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sakani.DA.DTOs;
using Sakani.DA.Interfaces;
using Sakani.Data.Models;
using Sakni.Sevices.Interfaces;

namespace Sakni.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<FeedbackDto> GetFeedbackByIdAsync(Guid id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            return feedback == null ? null : MapToDto(feedback);
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbacksByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null)
        {
            var feedbacks = await _feedbackRepository.GetFeedbacksByApartmentIdAsync(apartmentId, skip, take);
            return MapToDtoList(feedbacks);
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbacksByUserIdAsync(string userId, int? skip = null, int? take = null)
        {
            var feedbacks = await _feedbackRepository.GetFeedbacksByUserIdAsync(userId, skip, take);
            return MapToDtoList(feedbacks);
        }

        public async Task<FeedbackDto> CreateFeedbackAsync(CreateFeedbackDto request)
        {
            var entity = new Feedback
            {
                FeedbackId = Guid.NewGuid(),
                ApartmentId = request.ApartmentId,
                Rate = request.Rate,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _feedbackRepository.AddAsync(entity);
            return MapToDto(created);
        }

        public async Task<FeedbackDto> UpdateFeedbackAsync(Guid id, UpdateFeedbackDto request)
        {
            var existing = await _feedbackRepository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Rate = request.Rate ?? existing.Rate;
            existing.Comment = request.Comment ?? existing.Comment;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _feedbackRepository.UpdateAsync(existing);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteFeedbackAsync(Guid id)
        {
            return await _feedbackRepository.DeleteAsync(id);
        }

        public async Task<FeedbackDto> GetFeedbackWithDetailsAsync(Guid feedbackId)
        {
            var feedback = await _feedbackRepository.GetFeedbackWithDetailsAsync(feedbackId);
            return feedback == null ? null : MapToDto(feedback);
        }

        public async Task<bool> UpdateFeedbackRatingAsync(Guid feedbackId, int newRating)
        {
            return await _feedbackRepository.UpdateFeedbackRatingAsync(feedbackId, newRating);
        }

        public async Task<int> GetTotalFeedbacksCountAsync()
        {
            return await _feedbackRepository.GetTotalFeedbacksCountAsync();
        }

        public async Task<decimal> GetAverageRatingByApartmentIdAsync(Guid apartmentId)
        {
            return await _feedbackRepository.GetAverageRatingByApartmentIdAsync(apartmentId);
        }

        public async Task<IEnumerable<FeedbackDto>> GetFeedbacksByRatingRangeAsync(int minRating, int maxRating, int? skip = null, int? take = null)
        {
            var feedbacks = await _feedbackRepository.GetFeedbacksByRatingRangeAsync(minRating, maxRating, skip, take);
            return MapToDtoList(feedbacks);
        }

        public async Task<bool> FeedbackExistsByUserAndApartmentAsync(string userId, Guid apartmentId)
        {
            return await _feedbackRepository.FeedbackExistsByUserAndApartmentAsync(userId, apartmentId);
        }

        public async Task<FeedbackDto> GetFeedbackByUserAndApartmentAsync(string userId, Guid apartmentId)
        {
            var feedback = await _feedbackRepository.GetFeedbackByUserAndApartmentAsync(userId, apartmentId);
            return feedback == null ? null : MapToDto(feedback);
        }

        // ======= Helper methods for mapping =======

        private FeedbackDto MapToDto(Feedback entity)
        {
            return new FeedbackDto
            {
                FeedbackId = entity.FeedbackId,
                UserId = entity.UserId,
                ApartmentId = entity.ApartmentId,
                Rate = entity.Rate,
                Comment = entity.Comment,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        private IEnumerable<FeedbackDto> MapToDtoList(IEnumerable<Feedback> entities)
        {
            var list = new List<FeedbackDto>();
            foreach (var entity in entities)
            {
                list.Add(MapToDto(entity));
            }
            return list;
        }
    }
}
