using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA.Interfaces
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<IEnumerable<Room>> GetRoomsByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null);
        Task<Room> GetRoomWithBedsAsync(int roomId);
        Task<IEnumerable<Room>> GetAvailableRoomsAsync(int? skip = null, int? take = null);
        Task<bool> UpdateRoomStatusAsync(int roomId, bool isAvailable);
        Task<int> GetTotalRoomsCountAsync();
        Task<IEnumerable<Room>> GetRoomsByTypeAsync(string roomType, int? skip = null, int? take = null);
        Task<Room> GetRoomWithDetailsAsync(int roomId);
    }
}
 