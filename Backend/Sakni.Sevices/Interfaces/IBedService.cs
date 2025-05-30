using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.DA.DTOs;

namespace Sakni.Sevices.Interfaces
{
    public interface IBedService
    {
        Task<BedDto> GetBedByIdAsync(Guid id);
        Task<IEnumerable<BedDto>> GetBedsByRoomIdAsync(Guid roomId, int? skip = null, int? take = null);
        Task<BedDto> CreateBedAsync(CreateBedDto request);
        Task<BedDto> UpdateBedAsync(Guid id, UpdateBedDto request);
        Task<bool> DeleteBedAsync(Guid id);
        Task<BedDto> GetBedWithStudentAsync(Guid bedId);
        Task<IEnumerable<BedDto>> GetAvailableBedsAsync(int? skip = null, int? take = null);
        Task<bool> UpdateBedStatusAsync(Guid bedId, bool isVacant);
        Task<bool> AssignStudentToBedAsync(Guid bedId, Guid studentId);
        Task<bool> RemoveStudentFromBedAsync(Guid bedId);
        Task<int> GetTotalBedsCountAsync();
        Task<IEnumerable<BedDto>> GetBedsByPriceRangeAsync(decimal minPrice, decimal maxPrice, int? skip = null, int? take = null);
        Task<BedDto> GetBedWithDetailsAsync(Guid bedId);
        Task<bool> UpdateBedPriceAsync(Guid bedId, decimal newPrice);
    }
}
