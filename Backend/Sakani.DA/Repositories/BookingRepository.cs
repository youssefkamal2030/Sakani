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
    public class BookingRepository : IBookingRepository
    {
        private readonly SakaniDbContext _context;

        public BookingRepository(SakaniDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> AddAsync(Booking entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.Bookings.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync(Expression<Func<Booking, bool>> filter = null)
        {
            return await _context.Bookings.CountAsync(filter ?? (x => true));
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(object id)
        {
            return await _context.Bookings.AnyAsync(b => b.BookingId == (Guid)id);
        }

        public async Task<IEnumerable<Booking>> FindAsync(Expression<Func<Booking, bool>> predicate)
        {
            return await _context.Bookings.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetAllAsync(
            Expression<Func<Booking, bool>> filter = null,
            Func<IQueryable<Booking>, IOrderedQueryable<Booking>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null)
        {
            IQueryable<Booking> query = _context.Bookings;

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

        public async Task<Booking> GetByIdAsync(object id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByStudentIdAsync(Guid studentId, int? skip = null, int? take = null)
        {
            var query = _context.Bookings.Where(b => b.stuendtId == studentId);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null)
        {
            var query = _context.Bookings.Where(b => b.ApartmentId == apartmentId);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<Booking> GetBookingWithDetailsAsync(Guid bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Student)
                .Include(b => b.Apartment)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task<bool> UpdateBookingStatusAsync(Guid bookingId, string status)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;

            booking.Status = status;
            booking.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CancelBookingAsync(Guid bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;

            booking.Status = "Canceled";
            booking.Canceled = true;
            booking.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ConfirmBookingAsync(Guid bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;

            booking.Status = "Confirmed";
            booking.Confirmed = true;
            booking.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetTotalBookingsCountAsync()
        {
            return await _context.Bookings.CountAsync();
        }

        public async Task<IEnumerable<Booking>> GetPendingBookingsAsync(int? skip = null, int? take = null)
        {
            var query = _context.Bookings.Where(b => b.Status == "Pending");

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetConfirmedBookingsAsync(int? skip = null, int? take = null)
        {
            var query = _context.Bookings.Where(b => b.Status == "Confirmed" && b.Confirmed);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetCanceledBookingsAsync(int? skip = null, int? take = null)
        {
            var query = _context.Bookings.Where(b => b.Status == "Canceled" && b.Canceled);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<decimal> GetTotalBookingsAmountAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Bookings
                .Where(b => b.CreatedAt >= startDate && b.CreatedAt <= endDate)
                .SumAsync(b => b.TotalPrice ?? 0);
        }

        public async Task<Booking> UpdateAsync(Booking entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Bookings.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
} 