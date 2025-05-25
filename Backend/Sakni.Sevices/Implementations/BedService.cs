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
    public class BedService : IBedService
    {
        private readonly IBedRepository _bedRepository;
        private readonly IMapper _mapper;

        public BedService(IBedRepository bedRepository, IMapper mapper)
        {
            _bedRepository = bedRepository;
            _mapper = mapper;
        }

        public async Task<BedDto> CreateBedAsync(CreateBedDto request)
        {
            var bed = _mapper.Map<Bed>(request);
            bed.CreatedAt = DateTime.UtcNow;
            bed.UpdatedAt = DateTime.UtcNow;
            bed.IsVacant = true; // افتراضياً السرير فارغ عند الإنشاء
            var created = await _bedRepository.AddAsync(bed);
            return _mapper.Map<BedDto>(created);
        }

        public async Task<bool> DeleteBedAsync(int id)
        {
            return await _bedRepository.DeleteAsync(id);
        }

        public async Task<BedDto> GetBedByIdAsync(int id)
        {
            var bed = await _bedRepository.GetByIdAsync(id);
            return _mapper.Map<BedDto>(bed);
        }

        public async Task<BedDto> GetBedWithDetailsAsync(int bedId)
        {
            var bed = await _bedRepository.GetBedWithDetailsAsync(bedId);
            return _mapper.Map<BedDto>(bed);
        }

        public async Task<BedDto> GetBedWithStudentAsync(int bedId)
        {
            var bed = await _bedRepository.GetBedWithStudentAsync(bedId);
            return _mapper.Map<BedDto>(bed);
        }

        public async Task<IEnumerable<BedDto>> GetBedsByPriceRangeAsync(decimal minPrice, decimal maxPrice, int? skip = null, int? take = null)
        {
            var beds = await _bedRepository.GetBedsByPriceRangeAsync(minPrice, maxPrice, skip, take);
            return _mapper.Map<IEnumerable<BedDto>>(beds);
        }

        public async Task<IEnumerable<BedDto>> GetBedsByRoomIdAsync(int roomId, int? skip = null, int? take = null)
        {
            var beds = await _bedRepository.GetBedsByRoomIdAsync(roomId, skip, take);
            return _mapper.Map<IEnumerable<BedDto>>(beds);
        }

        public async Task<IEnumerable<BedDto>> GetAvailableBedsAsync(int? skip = null, int? take = null)
        {
            var beds = await _bedRepository.GetAvailableBedsAsync(skip, take);
            return _mapper.Map<IEnumerable<BedDto>>(beds);
        }

        public async Task<int> GetTotalBedsCountAsync()
        {
            return await _bedRepository.GetTotalBedsCountAsync();
        }

        public async Task<bool> AssignStudentToBedAsync(int bedId, Guid studentId)
        {
            return await _bedRepository.AssignStudentToBedAsync(bedId, studentId);
        }

        public async Task<bool> RemoveStudentFromBedAsync(int bedId)
        {
            return await _bedRepository.RemoveStudentFromBedAsync(bedId);
        }

        public async Task<bool> UpdateBedPriceAsync(int bedId, decimal newPrice)
        {
            return await _bedRepository.UpdateBedPriceAsync(bedId, newPrice);
        }

        public async Task<bool> UpdateBedStatusAsync(int bedId, bool isVacant)
        {
            return await _bedRepository.UpdateBedStatusAsync(bedId, isVacant);
        }

        public async Task<BedDto> UpdateBedAsync(int id, UpdateBedDto request)
        {
            var bed = await _bedRepository.GetByIdAsync(id);
            if (bed == null)
                return null;

            bed.Price = request.Price;
            bed.IsAC = request.IsAC;
            bed.IsVacant = request.IsVacant;
            bed.UpdatedAt = DateTime.UtcNow;

            var updated = await _bedRepository.UpdateAsync(bed);
            return _mapper.Map<BedDto>(updated);
        }
    }
}
