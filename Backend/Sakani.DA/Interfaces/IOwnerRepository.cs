using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA.Interfaces
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        Task<Owner> GetOwnerByUserIdAsync(string userId);
        Task<IEnumerable<Owner>> GetOwnersByVerificationStatusAsync(string status, int? skip = null, int? take = null);
        Task<bool> UpdateVerificationStatusAsync(Guid id, string status);
        Task<Owner> GetOwnerWithApartmentsAsync(Guid ownerId);
        Task<IEnumerable<Owner>> GetOwnersWithDetailsAsync(int? skip = null, int? take = null);
        Task<int> GetTotalOwnersCountAsync();
        Task<IEnumerable<Owner>> SearchOwnersAsync(string searchTerm, int? skip = null, int? take = null);
        Task<bool> UpdateOwnerProfileAsync(Owner owner);
        Task<IEnumerable<Owner>> GetPendingVerificationOwnersAsync(int? skip = null, int? take = null);
        Task<Owner> AddOwnerAsync(Owner owner);
        Task<Owner> UpdateOwnerAsync(Owner owner);
        Task<bool> DeleteOwnerAsync(Guid id);
        Task<Owner> GetOwnerByIdAsync(Guid id);
        Task<IEnumerable<Owner>> GetAllOwnersAsync(int? skip = null, int? take = null);
        Task<bool> OwnerExistsAsync(Guid id);
    }
} 