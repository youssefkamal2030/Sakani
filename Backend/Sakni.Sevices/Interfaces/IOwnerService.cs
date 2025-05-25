using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.DA.DTOs;

namespace Sakni.Sevices.Interfaces
{
    public interface IOwnerService
    {
        Task<OwnerDto> GetOwnerByIdAsync(Guid id);
        Task<OwnerDto> GetOwnerByUserIdAsync(string userId);
        Task<IEnumerable<OwnerDto>> GetAllOwnersAsync(int? skip = null, int? take = null);
        Task<IEnumerable<OwnerDto>> GetOwnersByVerificationStatusAsync(string status, int? skip = null, int? take = null);
        Task<OwnerDto> CreateOwnerAsync(CreateOwnerDto request);
        Task<OwnerDto> UpdateOwnerAsync(Guid id, UpdateOwnerDto request);
        Task<bool> DeleteOwnerAsync(Guid id);
        Task<bool> UpdateVerificationStatusAsync(Guid id, string status);
        Task<OwnerDto> GetOwnerWithApartmentsAsync(Guid ownerId);
        Task<IEnumerable<OwnerDto>> GetOwnersWithDetailsAsync(int? skip = null, int? take = null);
        Task<IEnumerable<OwnerDto>> SearchOwnersAsync(string searchTerm, int? skip = null, int? take = null);
        Task<IEnumerable<OwnerDto>> GetPendingVerificationOwnersAsync(int? skip = null, int? take = null);
        Task<int> GetTotalOwnersCountAsync();
    }
}
