using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.DA.DTOs;

namespace Sakni.Sevices.Interfaces
{
    public interface IRoomService
    {
        Task<RoomDto> GetRoomByIdAsync(int id);
        Task<IEnumerable<RoomDto>> GetRoomsByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null);
        Task<RoomDto> CreateRoomAsync(CreateRoomDto request);
        Task<RoomDto> UpdateRoomAsync(int id, UpdateRoomDto request);
        Task<bool> DeleteRoomAsync(int id);
        Task<RoomDto> GetRoomWithBedsAsync(int roomId);
        Task<int> GetTotalRoomsCountAsync();
        Task<IEnumerable<RoomDto>> GetRoomsByTypeAsync(string roomType, int? skip = null, int? take = null);
        Task<RoomDto> GetRoomWithDetailsAsync(int roomId);
        Task<bool> UpdateRoomCapacityAsync(int roomId, int numberOfBeds);
    }
}
