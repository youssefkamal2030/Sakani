using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA.Interfaces
{
    public interface IApartmentRepository : IGenericRepository<Apartment>
    {
        Task<IEnumerable<Apartment>> GetApartmentsByOwnerIdAsync(Guid ownerId, int? skip = null, int? take = null);
        Task<Apartment> GetApartmentWithDetailsAsync(Guid apartmentId);
        Task<IEnumerable<Apartment>> GetAvailableApartmentsAsync(int? skip = null, int? take = null);
        Task<IEnumerable<Apartment>> SearchApartmentsAsync(string searchTerm, int? skip = null, int? take = null);
        Task<IEnumerable<Apartment>> GetApartmentsByLocationAsync(string location, int? skip = null, int? take = null);
        Task<IEnumerable<Apartment>> GetApartmentsWithRoomsAsync(int? skip = null, int? take = null);
        Task<int> GetTotalApartmentsCountAsync();
        Task<bool> UpdateApartmentStatusAsync(Guid apartmentId, bool isAvailable);
        Task<IEnumerable<Apartment>> GetApartmentsWithFeedbacksAsync(int? skip = null, int? take = null);
        Task<decimal> GetAverageRatingAsync(Guid apartmentId);
    }
}
