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
    public class RoomRepository : IRoomRepository
    {
        private readonly SakaniDbContext _context;

        public RoomRepository(SakaniDbContext context)
        {
            _context = context;
        }

        public async Task<Room> AddAsync(Room entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.Rooms.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync(Expression<Func<Room, bool>> filter = null)
        {
            return await _context.Rooms.CountAsync(filter ?? (x => true));
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return false;

            _context.Rooms.Remove(room);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(object id)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == (int)id);
        }

        public async Task<IEnumerable<Room>> FindAsync(Expression<Func<Room, bool>> predicate)
        {
            return await _context.Rooms.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAllAsync(
            Expression<Func<Room, bool>> filter = null,
            Func<IQueryable<Room>, IOrderedQueryable<Room>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null)
        {
            IQueryable<Room> query = _context.Rooms;

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

        public async Task<Room> GetByIdAsync(object id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<IEnumerable<Room>> GetRoomsByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null)
        {
            var query = _context.Rooms.Where(r => r.ApartmentId == apartmentId);
            
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            
            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<Room> GetRoomWithBedsAsync(int roomId)
        {
            return await _context.Rooms
                .Include(r => r.Beds)
                .FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(int? skip = null, int? take = null)
        {
            var query = _context.Rooms
                .Where(r => r.Beds.Any(b => b.IsVacant));

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<bool> UpdateRoomStatusAsync(int roomId, bool isAvailable)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null) return false;

            // Update all beds in the room
            var beds = await _context.Beds.Where(b => b.RoomId == roomId).ToListAsync();
            foreach (var bed in beds)
            {
                bed.IsVacant = isAvailable;
            }

            room.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> GetTotalRoomsCountAsync()
        {
            return await _context.Rooms.CountAsync();
        }

        public async Task<IEnumerable<Room>> GetRoomsByTypeAsync(string roomType, int? skip = null, int? take = null)
        {
            var query = _context.Rooms.Where(r => r.RoomType == roomType);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<Room> GetRoomWithDetailsAsync(int roomId)
        {
            return await _context.Rooms
                .Include(r => r.Beds)
                .Include(r => r.Apartment)
                .FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task<bool> UpdateRoomCapacityAsync(int roomId, int numberOfBeds)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null) return false;

            room.NumberOfBeds = numberOfBeds;
            room.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Room> UpdateAsync(Room entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Rooms.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
} 