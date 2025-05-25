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
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly SakaniDbContext _context;

        public ApartmentRepository(SakaniDbContext context)
        {
            _context = context;
        }

        public async Task<Apartment> AddAsync(Apartment entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.Apartments.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync(Expression<Func<Apartment, bool>> filter = null)
        {
            return await _context.Apartments.CountAsync(filter ?? (x => true));
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null) return false;

            _context.Apartments.Remove(apartment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(object id)
        {
            return await _context.Apartments.AnyAsync(a => a.ApartmentId == (Guid)id);
        }

        public async Task<IEnumerable<Apartment>> FindAsync(Expression<Func<Apartment, bool>> predicate)
        {
            return await _context.Apartments.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Apartment>> GetAllAsync(
            Expression<Func<Apartment, bool>> filter = null,
            Func<IQueryable<Apartment>, IOrderedQueryable<Apartment>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null)
        {
            IQueryable<Apartment> query = _context.Apartments;

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

        public async Task<Apartment> GetByIdAsync(object id)
        {
            return await _context.Apartments.FindAsync(id);
        }

        public async Task<IEnumerable<Apartment>> GetApartmentsByOwnerIdAsync(Guid ownerId, int? skip = null, int? take = null)
        {
            var query = _context.Apartments.Where(a => a.OwnerId == ownerId);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<Apartment> GetApartmentWithDetailsAsync(Guid apartmentId)
        {
            return await _context.Apartments
                .Include(a => a.Owner)
                .Include(a => a.Rooms)
                .ThenInclude(r => r.Beds)
                .Include(a => a.Feedbacks)
                .FirstOrDefaultAsync(a => a.ApartmentId == apartmentId);
        }

        public async Task<IEnumerable<Apartment>> GetAvailableApartmentsAsync(int? skip = null, int? take = null)
        {
            var query = _context.Apartments
                .Where(a => a.Rooms.Any(r => r.Beds.Any(b => b.IsVacant)));

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Apartment>> SearchApartmentsAsync(string searchTerm, int? skip = null, int? take = null)
        {
            var query = _context.Apartments
                .Where(a => a.ApartmentTitle.Contains(searchTerm) || 
                           a.Description.Contains(searchTerm) || 
                           a.Location.Contains(searchTerm));

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Apartment>> GetApartmentsByLocationAsync(string location, int? skip = null, int? take = null)
        {
            var query = _context.Apartments
                .Where(a => a.Location.Contains(location));

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Apartment>> GetApartmentsWithRoomsAsync(int? skip = null, int? take = null)
        {
            var query = _context.Apartments
                .Include(a => a.Rooms)
                .ThenInclude(r => r.Beds);

            if (skip.HasValue)
                query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Apartment, ICollection<Bed>>)query.Skip(skip.Value);

            if (take.HasValue)
                query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Apartment, ICollection<Bed>>)query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<int> GetTotalApartmentsCountAsync()
        {
            return await _context.Apartments.CountAsync();
        }

        public async Task<bool> UpdateApartmentStatusAsync(Guid apartmentId, bool isAvailable)
        {
            var apartment = await _context.Apartments.FindAsync(apartmentId);
            if (apartment == null) return false;

            // Update all beds in the apartment to match the availability
            var beds = await _context.Beds
                .Where(b => b.Room.ApartmentId == apartmentId)
                .ToListAsync();

            foreach (var bed in beds)
            {
                bed.IsVacant = isAvailable;
            }

            apartment.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Apartment>> GetApartmentsWithFeedbacksAsync(int? skip = null, int? take = null)
        {
            var query = _context.Apartments
                .Include(a => a.Feedbacks)
                .Where(a => a.Feedbacks.Any());

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<decimal> GetAverageRatingAsync(Guid apartmentId)
        {
            return (decimal)await _context.Feedbacks
                .Where(f => f.ApartmentId == apartmentId)
                .AverageAsync(f => f.Rate ?? 0);
        }

        public async Task<Apartment> UpdateAsync(Apartment entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Apartments.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}