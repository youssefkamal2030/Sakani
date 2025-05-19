using Microsoft.EntityFrameworkCore;
using Sakani.DA.Data;
using Sakani.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Owner> AddOwner(Owner owner)
        {
            owner.CreatedAt = DateTime.UtcNow;
            owner.UpdatedAt = DateTime.UtcNow;
            
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();
            return owner;
        }

        public async Task<bool> DeleteOwner(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
                return false;

            _context.Owners.Remove(owner);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Owner>> GetAllOwners()
        {
            return await _context.Owners.ToListAsync();
        }

        public async Task<Owner> GetOwnerById(int id)
        {
            return await _context.Owners.FindAsync(id);
        }

        public async Task<Owner> GetOwnerByUserId(string userId)
        {
            return await _context.Owners
                .FirstOrDefaultAsync(o => o.UserId == userId);
        }

        public async Task<IEnumerable<Owner>> GetOwnersByVerificationStatus(string status)
        {
            return await _context.Owners
                .Where(o => o.VerificationStatus == status)
                .ToListAsync();
        }

        public async Task<bool> OwnerExists(int id)
        {
            return await _context.Owners.AnyAsync(o => o.OwnerId == id);
        }

        public async Task<Owner> UpdateOwner(Owner owner)
        {
            owner.UpdatedAt = DateTime.UtcNow;
            
            _context.Owners.Update(owner);
            await _context.SaveChangesAsync();
            return owner;
        }

        public async Task<bool> UpdateVerificationStatus(int id, string status)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
                return false;

            owner.VerificationStatus = status;
            owner.UpdatedAt = DateTime.UtcNow;
            
            _context.Owners.Update(owner);
            return await _context.SaveChangesAsync() > 0;
        }
    }
} 