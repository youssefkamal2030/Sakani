using AutoMapper;
using Sakani.DA.DTOs;
using Sakani.DA.Interfaces;
using Sakani.Data.Models;
using Sakni.Sevices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sakni.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<RoomDto> CreateRoomAsync(CreateRoomDto request)
        {
            try
            {
                var room = _mapper.Map<Room>(request);
                room.CreatedAt = DateTime.UtcNow;
                room.UpdatedAt = DateTime.UtcNow;

                var created = await _roomRepository.AddAsync(room);
                return _mapper.Map<RoomDto>(created);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> DeleteRoomAsync(Guid id)
        {
            return await _roomRepository.DeleteAsync(id);
        }

        public async Task<RoomDto> GetRoomByIdAsync(Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            return _mapper.Map<RoomDto>(room);
        }

        public async Task<RoomDto> GetRoomWithBedsAsync(Guid roomId)
        {
            var room = await _roomRepository.GetRoomWithBedsAsync(roomId);
            return _mapper.Map<RoomDto>(room);
        }

        public async Task<int> GetTotalRoomsCountAsync()
        {
            return await _roomRepository.GetTotalRoomsCountAsync();
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null)
        {
            var rooms = await _roomRepository.GetRoomsByApartmentIdAsync(apartmentId, skip, take);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByTypeAsync(string roomType, int? skip = null, int? take = null)
        {
            var rooms = await _roomRepository.GetRoomsByTypeAsync(roomType, skip, take);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<RoomDto> GetRoomWithDetailsAsync(Guid roomId)
        {
            var room = await _roomRepository.GetRoomWithDetailsAsync(roomId);
            return _mapper.Map<RoomDto>(room);
        }

        public async Task<bool> UpdateRoomCapacityAsync(Guid roomId, int numberOfBeds)
        {
            return await _roomRepository.UpdateRoomCapacityAsync(roomId, numberOfBeds);
        }

        public async Task<RoomDto> UpdateRoomAsync(Guid id, UpdateRoomDto request)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return null;

            room.RoomType = request.RoomType;
            room.NumberOfBeds = request.NumberOfBeds;
            room.UpdatedAt = DateTime.UtcNow;

            var updated = await _roomRepository.UpdateAsync(room);
            return _mapper.Map<RoomDto>(updated);
        }
    }
}
