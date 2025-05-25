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
        Task<BedDto> GetBedByIdAsync(int id);
        Task<IEnumerable<BedDto>> GetBedsByRoomIdAsync(int roomId, int? skip = null, int? take = null);
        Task<BedDto> CreateBedAsync(CreateBedDto request);
        Task<BedDto> UpdateBedAsync(int id, UpdateBedDto request);
        Task<bool> DeleteBedAsync(int id);
        Task<BedDto> GetBedWithStudentAsync(int bedId);
        Task<IEnumerable<BedDto>> GetAvailableBedsAsync(int? skip = null, int? take = null);
        Task<bool> UpdateBedStatusAsync(int bedId, bool isVacant);
        Task<bool> AssignStudentToBedAsync(int bedId, Guid studentId);
        Task<bool> RemoveStudentFromBedAsync(int bedId);
        Task<int> GetTotalBedsCountAsync();
        Task<IEnumerable<BedDto>> GetBedsByPriceRangeAsync(decimal minPrice, decimal maxPrice, int? skip = null, int? take = null);
        Task<BedDto> GetBedWithDetailsAsync(int bedId);
        Task<bool> UpdateBedPriceAsync(int bedId, decimal newPrice);
    }
}
