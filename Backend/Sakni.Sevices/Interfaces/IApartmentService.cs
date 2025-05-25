using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.DA.DTOs;

namespace Sakni.Sevices.Interfaces
{
    public interface IApartmentService
    {
        Task<ApartmentDto> GetApartmentByIdAsync(Guid id);
        Task<IEnumerable<ApartmentDto>> GetAllApartmentsAsync(int? skip = null, int? take = null);
        Task<IEnumerable<ApartmentDto>> GetApartmentsByOwnerIdAsync(Guid ownerId, int? skip = null, int? take = null);
        Task<ApartmentDto> CreateApartmentAsync(CreateApartmentDto request);
        Task<ApartmentDto> UpdateApartmentAsync(Guid id, UpdateApartmentDto request);
        Task<bool> DeleteApartmentAsync(Guid id);
        Task<ApartmentDto> GetApartmentWithDetailsAsync(Guid apartmentId);
        Task<IEnumerable<ApartmentDto>> SearchApartmentsAsync(string searchTerm, int? skip = null, int? take = null);
        Task<IEnumerable<ApartmentDto>> GetApartmentsByLocationAsync(string location, int? skip = null, int? take = null);
        Task<IEnumerable<ApartmentDto>> GetApartmentsWithRoomsAsync(int? skip = null, int? take = null);
        Task<IEnumerable<ApartmentDto>> GetApartmentsWithFeedbacksAsync(int? skip = null, int? take = null);
        Task<int> GetTotalApartmentsCountAsync();
    }
}
