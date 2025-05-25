using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA.Interfaces
{
    public interface IBedRepository : IGenericRepository<Bed>
    {
        Task<IEnumerable<Bed>> GetBedsByRoomIdAsync(int roomId, int? skip = null, int? take = null);
        Task<Bed> GetBedWithStudentAsync(int bedId);
        Task<IEnumerable<Bed>> GetAvailableBedsAsync(int? skip = null, int? take = null);
        Task<bool> UpdateBedStatusAsync(int bedId, bool isVacant);
        Task<bool> AssignStudentToBedAsync(int bedId, Guid studentId);
        Task<bool> RemoveStudentFromBedAsync(int bedId);
        Task<int> GetTotalBedsCountAsync();
        Task<IEnumerable<Bed>> GetBedsByPriceRangeAsync(decimal minPrice, decimal maxPrice, int? skip = null, int? take = null);
        Task<Bed> GetBedWithDetailsAsync(int bedId);
        Task<bool> UpdateBedPriceAsync(int bedId, decimal newPrice);
    }
} 