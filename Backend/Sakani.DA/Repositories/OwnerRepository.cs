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
    public class OwnerRepository : IOwnerRepository
    {
        private readonly SakaniDbContext _context;

        public OwnerRepository(SakaniDbContext context)
        {
            _context = context;
        }

        public async Task<Owner> AddAsync(Owner entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.Owners.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Owner> AddOwnerAsync(Owner owner)
        {
            return await AddAsync(owner);
        }

        public async Task<int> CountAsync(Expression<Func<Owner, bool>> filter = null)
        {
            return await _context.Owners.CountAsync(filter ?? (x => true));
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null) return false;

            _context.Owners.Remove(owner);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteOwnerAsync(Guid id)
        {
            return await DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(object id)
        {
            return await _context.Owners.AnyAsync(o => o.OwnerId == (Guid)id);
        }

        public async Task<bool> OwnerExistsAsync(Guid id)
        {
            return await ExistsAsync(id);
        }

        public async Task<IEnumerable<Owner>> FindAsync(Expression<Func<Owner, bool>> predicate)
        {
            return await _context.Owners.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Owner>> GetAllAsync(
            Expression<Func<Owner, bool>> filter = null,
            Func<IQueryable<Owner>, IOrderedQueryable<Owner>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null)
        {
            IQueryable<Owner> query = _context.Owners;

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

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync(int? skip = null, int? take = null)
        {
            return await GetAllAsync(null, null, "", skip, take);
        }

        public async Task<Owner> GetByIdAsync(object id)
        {
            return await _context.Owners.FindAsync(id);
        }

        public async Task<Owner> GetOwnerByIdAsync(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<Owner> GetOwnerByUserIdAsync(string userId)
        {
            return await _context.Owners
                .FirstOrDefaultAsync(o => o.UserId.ToString() == userId);
        }

        public async Task<Owner> GetOwnerWithApartmentsAsync(Guid ownerId)
        {
            return await _context.Owners
                .Include(o => o.Apartments)
                .FirstOrDefaultAsync(o => o.OwnerId == ownerId);
        }

        public async Task<IEnumerable<Owner>> GetOwnersByVerificationStatusAsync(string status, int? skip = null, int? take = null)
        {
            var query = _context.Owners.Where(o => o.VerificationStatus == status);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Owner>> GetOwnersWithDetailsAsync(int? skip = null, int? take = null)
        {
            var query = _context.Owners
                .Include(o => o.Apartments)
                .ThenInclude(a => a.Rooms)
                .ThenInclude(r => r.Beds);

            if (skip.HasValue)
                query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Owner, ICollection<Bed>>)query.Skip(skip.Value);

            if (take.HasValue)
                query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Owner, ICollection<Bed>>)query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Owner>> GetPendingVerificationOwnersAsync(int? skip = null, int? take = null)
        {
            var query = _context.Owners.Where(o => o.VerificationStatus == "Pending");

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<int> GetTotalOwnersCountAsync()
        {
            return await _context.Owners.CountAsync();
        }

        public async Task<IEnumerable<Owner>> SearchOwnersAsync(string searchTerm, int? skip = null, int? take = null)
        { var query = _context.Owners
        .Include(o => o.User)  
        .Where(o => o.FirstName.Contains(searchTerm) || 
                   o.LastName.Contains(searchTerm) || 
                   o.User.Email.Contains(searchTerm) ||  
                   o.User.UserName.Contains(searchTerm)); 

    if (skip.HasValue)
        query = query.Skip(skip.Value);

    if (take.HasValue)
        query = query.Take(take.Value);

    return await query.ToListAsync();
        }

        public async Task<bool> UpdateOwnerProfileAsync(Owner owner)
        {
            owner.UpdatedAt = DateTime.UtcNow;
            _context.Owners.Update(owner);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Owner> UpdateAsync(Owner entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Owners.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Owner> UpdateOwnerAsync(Owner owner)
        {
            return await UpdateAsync(owner);
        }

        public async Task<bool> UpdateVerificationStatusAsync(Guid id, string status)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null) return false;

            owner.VerificationStatus = status;
            owner.UpdatedAt = DateTime.UtcNow;
            
            _context.Owners.Update(owner);
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 