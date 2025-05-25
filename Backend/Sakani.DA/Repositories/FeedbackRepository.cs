using Microsoft.EntityFrameworkCore;
using Sakani.DA.Data;
using Sakani.DA.Interfaces;
using Sakani.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sakani.DA.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly SakaniDbContext _context;

        public FeedbackRepository(SakaniDbContext context)
        {
            _context = context;
        }

        public async Task<Feedback> AddAsync(Feedback entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.Feedbacks.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync(Expression<Func<Feedback, bool>> filter = null)
        {
            return await _context.Feedbacks.CountAsync(filter ?? (x => true));
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null) return false;

            _context.Feedbacks.Remove(feedback);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(object id)
        {
            return await _context.Feedbacks.AnyAsync(f => f.FeedbackId == (Guid)id);
        }

        public async Task<IEnumerable<Feedback>> FindAsync(Expression<Func<Feedback, bool>> predicate)
        {
            return await _context.Feedbacks.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetAllAsync(
            Expression<Func<Feedback, bool>> filter = null,
            Func<IQueryable<Feedback>, IOrderedQueryable<Feedback>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null)
        {
            IQueryable<Feedback> query = _context.Feedbacks;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<Feedback> GetByIdAsync(object id)
        {
            return await _context.Feedbacks.FindAsync(id);
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null)
        {
            var query = _context.Feedbacks.Where(f => f.ApartmentId == apartmentId);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksByUserIdAsync(string userId, int? skip = null, int? take = null)
        {
            var query = _context.Feedbacks.Where(f => f.UserId == userId);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<Feedback> GetFeedbackWithDetailsAsync(Guid feedbackId)
        {
            return await _context.Feedbacks
                .Include(f => f.User)
                .Include(f => f.Apartment)
                .FirstOrDefaultAsync(f => f.FeedbackId == feedbackId);
        }

        public async Task<bool> UpdateFeedbackRatingAsync(Guid feedbackId, int newRating)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback == null) return false;

            feedback.Rate = newRating;
            feedback.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetTotalFeedbacksCountAsync()
        {
            return await _context.Feedbacks.CountAsync();
        }

        public async Task<decimal> GetAverageRatingByApartmentIdAsync(Guid apartmentId)
        {
            return (decimal)await _context.Feedbacks
                .Where(f => f.ApartmentId == apartmentId)
                .AverageAsync(f => f.Rate);
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacksByRatingRangeAsync(int minRating, int maxRating, int? skip = null, int? take = null)
        {
            var query = _context.Feedbacks.Where(f => f.Rate >= minRating && f.Rate <= maxRating);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<bool> FeedbackExistsByUserAndApartmentAsync(string userId, Guid apartmentId)
        {
            return await _context.Feedbacks
                .AnyAsync(f => f.UserId == userId && f.ApartmentId == apartmentId);
        }

        public async Task<Feedback> GetFeedbackByUserAndApartmentAsync(string userId, Guid apartmentId)
        {
            return await _context.Feedbacks
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ApartmentId == apartmentId);
        }

        public async Task<Feedback> UpdateAsync(Feedback entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Feedbacks.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
} 