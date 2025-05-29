using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA.Interfaces
{
    public interface IBedRepository : IGenericRepository<Bed>
    {
        Task<IEnumerable<Bed>> GetBedsByRoomIdAsync(int roomId, int? skip = null, int? take = null);
        Task<Bed> GetBedWithStudentAsync(Guid bedId);
        Task<IEnumerable<Bed>> GetAvailableBedsAsync(int? skip = null, int? take = null);
        Task<bool> UpdateBedStatusAsync(Guid bedId, bool isVacant);
        Task<bool> AssignStudentToBedAsync(Guid bedId, Guid studentId);
        Task<bool> RemoveStudentFromBedAsync(Guid bedId);
        Task<int> GetTotalBedsCountAsync();
        Task<IEnumerable<Bed>> GetBedsByPriceRangeAsync(decimal minPrice, decimal maxPrice, int? skip = null, int? take = null);
        Task<Bed> GetBedWithDetailsAsync(Guid bedId);
        Task<bool> UpdateBedPriceAsync(Guid bedId, decimal newPrice);
    }
} 