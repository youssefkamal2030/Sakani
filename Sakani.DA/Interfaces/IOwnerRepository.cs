using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllOwners();
        Task<Owner> GetOwnerById(int id);
        Task<Owner> GetOwnerByUserId(string userId);
        Task<Owner> AddOwner(Owner owner);
        Task<Owner> UpdateOwner(Owner owner);
        Task<bool> DeleteOwner(int id);
        Task<bool> OwnerExists(int id);
        Task<IEnumerable<Owner>> GetOwnersByVerificationStatus(string status);
        Task<bool> UpdateVerificationStatus(int id, string status);
    }
} 