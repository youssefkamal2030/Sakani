using AutoMapper;
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
    public class OwnerService : IOwnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OwnerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OwnerDto> CreateOwnerAsync(CreateOwnerDto request)
        {
            var owner = _mapper.Map<Owner>(request);
            var createdOwner = await _unitOfWork.Owners.AddAsync(owner);
            return _mapper.Map<OwnerDto>(createdOwner);
        }

        public async Task<bool> DeleteOwnerAsync(Guid id)
        {
            return await _unitOfWork.Owners.DeleteAsync(id);
        }

        public async Task<IEnumerable<OwnerDto>> GetAllOwnersAsync(int? skip = null, int? take = null)
        {
            var owners = await _unitOfWork.Owners.GetAllAsync(skip: skip, take: take);
            return _mapper.Map<IEnumerable<OwnerDto>>(owners);
        }

        public async Task<OwnerDto> GetOwnerByIdAsync(Guid id)
        {
            var owner = await _unitOfWork.Owners.GetByIdAsync(id);
            return owner == null ? null : _mapper.Map<OwnerDto>(owner);
        }

        public async Task<OwnerDto> GetOwnerByUserIdAsync(string userId)
        {
            var owner = await _unitOfWork.Owners.GetOwnerByUserIdAsync(userId);
            return owner == null ? null : _mapper.Map<OwnerDto>(owner);
        }

        public async Task<IEnumerable<OwnerDto>> GetOwnersByVerificationStatusAsync(string status, int? skip = null, int? take = null)
        {
            var owners = await _unitOfWork.Owners.GetOwnersByVerificationStatusAsync(status, skip, take);
            return _mapper.Map<IEnumerable<OwnerDto>>(owners);
        }

        public async Task<OwnerDto> UpdateOwnerAsync(Guid id, UpdateOwnerDto request)
        {
            var existing = await _unitOfWork.Owners.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(request, existing);
            var updated = await _unitOfWork.Owners.UpdateAsync(existing);
            return _mapper.Map<OwnerDto>(updated);
        }

        public async Task<bool> UpdateVerificationStatusAsync(Guid id, string status)
        {
            return await _unitOfWork.Owners.UpdateVerificationStatusAsync(id, status);
        }

        public async Task<OwnerDto> GetOwnerWithApartmentsAsync(Guid ownerId)
        {
            var owner = await _unitOfWork.Owners.GetOwnerWithApartmentsAsync(ownerId);
            return owner == null ? null : _mapper.Map<OwnerDto>(owner);
        }

        public async Task<IEnumerable<OwnerDto>> GetOwnersWithDetailsAsync(int? skip = null, int? take = null)
        {
            var owners = await _unitOfWork.Owners.GetOwnersWithDetailsAsync(skip, take);
            return _mapper.Map<IEnumerable<OwnerDto>>(owners);
        }

        public async Task<IEnumerable<OwnerDto>> SearchOwnersAsync(string searchTerm, int? skip = null, int? take = null)
        {
            var owners = await _unitOfWork.Owners.SearchOwnersAsync(searchTerm, skip, take);
            return _mapper.Map<IEnumerable<OwnerDto>>(owners);
        }

        public async Task<IEnumerable<OwnerDto>> GetPendingVerificationOwnersAsync(int? skip = null, int? take = null)
        {
            var owners = await _unitOfWork.Owners.GetOwnersByVerificationStatusAsync("Pending", skip, take);
            return _mapper.Map<IEnumerable<OwnerDto>>(owners);
        }

        public async Task<int> GetTotalOwnersCountAsync()
        {
            return await _unitOfWork.Owners.GetTotalOwnersCountAsync();
        }
    }
}
