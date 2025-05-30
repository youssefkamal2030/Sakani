using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Sakani.DA.DTOs;
using Sakani.DA.Interfaces;
using Sakani.Data.Models;
using Sakni.Sevices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakni.Services.Implementations
{
    public class ApartmentService : IApartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApartmentDto> CreateApartmentAsync(CreateApartmentDto request)
        {
            try
            {
                var apartment = _mapper.Map<Apartment>(request);
                var createdApartment = await _unitOfWork.Apartments.AddAsync(apartment);
                return _mapper.Map<ApartmentDto>(createdApartment);
            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
               
            }
            
        }

        public async Task<bool> DeleteApartmentAsync(Guid id)
        {
            return await _unitOfWork.Apartments.DeleteAsync(id);
        }

        public async Task<IEnumerable<ApartmentDto>> GetAllApartmentsAsync(int? skip = null, int? take = null)
        {
            var apartments = await _unitOfWork.Apartments.GetAllAsync(skip: skip, take: take);
            return _mapper.Map<IEnumerable<ApartmentDto>>(apartments);
        }

        public async Task<ApartmentDto> GetApartmentByIdAsync(Guid id)
        {
            var apartment = await _unitOfWork.Apartments.GetByIdAsync(id);
            return apartment == null ? null : _mapper.Map<ApartmentDto>(apartment);
        }

        public async Task<ApartmentDto> UpdateApartmentAsync(Guid id, UpdateApartmentDto request)
        {
            var existing = await _unitOfWork.Apartments.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(request, existing);
            var updated = await _unitOfWork.Apartments.UpdateAsync(existing);
            return _mapper.Map<ApartmentDto>(updated);
        }

        public async Task<ApartmentDto> GetApartmentWithDetailsAsync(Guid apartmentId)
        {
            var apartment = await _unitOfWork.Apartments.GetApartmentWithDetailsAsync(apartmentId);
            return apartment == null ? null : _mapper.Map<ApartmentDto>(apartment);
        }

        public async Task<IEnumerable<ApartmentDto>> SearchApartmentsAsync(string searchTerm, int? skip = null, int? take = null)
        {
            var apartments = await _unitOfWork.Apartments.SearchApartmentsAsync(searchTerm, skip, take);
            return _mapper.Map<IEnumerable<ApartmentDto>>(apartments);
        }

        public async Task<IEnumerable<ApartmentDto>> GetApartmentsByOwnerIdAsync(Guid ownerId, int? skip = null, int? take = null)
        {
            var apartments = await _unitOfWork.Apartments.GetApartmentsByOwnerIdAsync(ownerId, skip, take);
            return _mapper.Map<IEnumerable<ApartmentDto>>(apartments);
        }

        public async Task<IEnumerable<ApartmentDto>> GetApartmentsByLocationAsync(string location, int? skip = null, int? take = null)
        {
            var apartments = await _unitOfWork.Apartments.GetApartmentsByLocationAsync(location, skip, take);
            return _mapper.Map<IEnumerable<ApartmentDto>>(apartments);
        }

        public async Task<IEnumerable<ApartmentDto>> GetApartmentsWithRoomsAsync(int? skip = null, int? take = null)
        {
            var apartments = await _unitOfWork.Apartments.GetApartmentsWithRoomsAsync(skip, take);
            return _mapper.Map<IEnumerable<ApartmentDto>>(apartments);
        }

        public async Task<IEnumerable<ApartmentDto>> GetApartmentsWithFeedbacksAsync(int? skip = null, int? take = null)
        {
            var apartments = await _unitOfWork.Apartments.GetApartmentsWithFeedbacksAsync(skip, take);
            return _mapper.Map<IEnumerable<ApartmentDto>>(apartments);
        }

        public async Task<int> GetTotalApartmentsCountAsync()
        {
            return await _unitOfWork.Apartments.GetTotalApartmentsCountAsync();
        }
    }
}
