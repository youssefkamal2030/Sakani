using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA
{
    public interface IApartmentRepository
    {
        Task<IEnumerable<Apartment>> GetAllApartments();
        Task<Apartment> GetApartmentById(int id);
        Task<Apartment> AddApartment(Apartment apartment);
        Task<Apartment> UpdateApartment(Apartment apartment);
        Task<bool> DeleteApartment(int id);
        Task<IEnumerable<Apartment>> GetApartmentsByOwnerId(string ownerId);
        Task<bool> ApartmentExists(int id);
    }
}
