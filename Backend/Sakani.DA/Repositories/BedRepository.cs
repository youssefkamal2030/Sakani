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
    public class BedRepository : IBedRepository
    {
        private readonly SakaniDbContext _context;

        public BedRepository(SakaniDbContext context)
        {
            _context = context;
        }

        public async Task<Bed> AddAsync(Bed entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.Beds.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync(Expression<Func<Bed, bool>> filter = null)
        {
            return await _context.Beds.CountAsync(filter ?? (x => true));
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var bed = await _context.Beds.FindAsync(id);
            if (bed == null) return false;

            _context.Beds.Remove(bed);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(object id)
        {
            return await _context.Beds.AnyAsync(b => b.Id == (Guid)id);
        }

        public async Task<IEnumerable<Bed>> FindAsync(Expression<Func<Bed, bool>> predicate)
        {
            return await _context.Beds.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Bed>> GetAllAsync(
            Expression<Func<Bed, bool>> filter = null,
            Func<IQueryable<Bed>, IOrderedQueryable<Bed>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null)
        {
            IQueryable<Bed> query = _context.Beds;

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

        public async Task<Bed> GetByIdAsync(object id)
        {
            return await _context.Beds.FindAsync(id);
        }

        public async Task<IEnumerable<Bed>> GetBedsByRoomIdAsync(Guid roomId, int? skip = null, int? take = null)
        {
            var query = _context.Beds.Where(b => b.RoomId == roomId);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<Bed> GetBedWithStudentAsync(Guid bedId)
        {
            return await _context.Beds
                .Include(b => b.Student)
                .FirstOrDefaultAsync(b => b.Id == bedId);
        }

        public async Task<IEnumerable<Bed>> GetAvailableBedsAsync(int? skip = null, int? take = null)
        {
            var query = _context.Beds.Where(b => b.IsVacant);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<bool> UpdateBedStatusAsync(Guid bedId, bool isVacant)
        {
            var bed = await _context.Beds.FindAsync(bedId);
            if (bed == null) return false;

            bed.IsVacant = isVacant;
            bed.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AssignStudentToBedAsync(Guid bedId, Guid studentId)
        {
            var bed = await _context.Beds.FindAsync(bedId);
            if (bed == null || !bed.IsVacant) return false;

            bed.studentId = studentId;
            bed.IsVacant = false;
            bed.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveStudentFromBedAsync(Guid bedId)
        {
            var bed = await _context.Beds.FindAsync(bedId);
            if (bed == null) return false;

            bed.studentId = null;
            bed.IsVacant = true;
            bed.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetTotalBedsCountAsync()
        {
            return await _context.Beds.CountAsync();
        }

        public async Task<IEnumerable<Bed>> GetBedsByPriceRangeAsync(decimal minPrice, decimal maxPrice, int? skip = null, int? take = null)
        {
            var query = _context.Beds.Where(b => b.Price >= minPrice && b.Price <= maxPrice);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<Bed> GetBedWithDetailsAsync(Guid bedId)
        {
            return await _context.Beds
                .Include(b => b.Student)
                .Include(b => b.Room)
                .ThenInclude(r => r.Apartment)
                .FirstOrDefaultAsync(b => b.Id == bedId);
        }

        public async Task<bool> UpdateBedPriceAsync(Guid bedId, decimal newPrice)
        {
            var bed = await _context.Beds.FindAsync(bedId);
            if (bed == null) return false;

            bed.Price = newPrice;
            bed.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Bed> UpdateAsync(Bed entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Beds.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
} 