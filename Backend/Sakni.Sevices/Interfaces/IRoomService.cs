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
        Task<RoomDto> GetRoomByIdAsync(Guid id);
        Task<IEnumerable<RoomDto>> GetRoomsByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null);
        Task<RoomDto> CreateRoomAsync(CreateRoomDto request);
        Task<RoomDto> UpdateRoomAsync(Guid id, UpdateRoomDto request);
        Task<bool> DeleteRoomAsync(Guid id);
        Task<RoomDto> GetRoomWithBedsAsync(Guid roomId);
        Task<int> GetTotalRoomsCountAsync();
        Task<IEnumerable<RoomDto>> GetRoomsByTypeAsync(string roomType, int? skip = null, int? take = null);
        Task<RoomDto> GetRoomWithDetailsAsync(Guid roomId);
        Task<bool> UpdateRoomCapacityAsync(Guid roomId, int numberOfBeds);
    }
}
