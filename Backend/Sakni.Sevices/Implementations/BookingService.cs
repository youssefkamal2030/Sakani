using AutoMapper;
using Sakani.DA.DTOs;
using Sakani.DA.Interfaces;
using Sakani.Data.Models;
using Sakni.Sevices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sakni.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingDto request)
        {
            var booking = new Booking
            {
                ApartmentId = request.ApartmentId,
                Notes = request.Notes,
                Status = "Pending",
                Confirmed = false,
                Canceled = false,
                BookingDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var createdBooking = await _bookingRepository.AddAsync(booking);

            return _mapper.Map<BookingDto>(createdBooking);
        }

        public async Task<bool> CancelBookingAsync(Guid bookingId)
        {
            return await _bookingRepository.CancelBookingAsync(bookingId);
        }

        public async Task<bool> ConfirmBookingAsync(Guid bookingId)
        {
            return await _bookingRepository.ConfirmBookingAsync(bookingId);
        }

        public async Task<bool> DeleteBookingAsync(Guid id)
        {
            return await _bookingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null)
        {
            var bookings = await _bookingRepository.GetBookingsByApartmentIdAsync(apartmentId, skip, take);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByStudentIdAsync(Guid studentId, int? skip = null, int? take = null)
        {
            var bookings = await _bookingRepository.GetBookingsByStudentIdAsync(studentId, skip, take);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetBookingByIdAsync(Guid id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<BookingDto> GetBookingWithDetailsAsync(Guid bookingId)
        {
            var booking = await _bookingRepository.GetBookingWithDetailsAsync(bookingId);
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<IEnumerable<BookingDto>> GetCanceledBookingsAsync(int? skip = null, int? take = null)
        {
            var bookings = await _bookingRepository.GetCanceledBookingsAsync(skip, take);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<IEnumerable<BookingDto>> GetConfirmedBookingsAsync(int? skip = null, int? take = null)
        {
            var bookings = await _bookingRepository.GetConfirmedBookingsAsync(skip, take);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<int> GetTotalBookingsCountAsync()
        {
            return await _bookingRepository.GetTotalBookingsCountAsync();
        }

        public async Task<decimal> GetTotalBookingsAmountAsync(DateTime startDate, DateTime endDate)
        {
            return await _bookingRepository.GetTotalBookingsAmountAsync(startDate, endDate);
        }

        public async Task<IEnumerable<BookingDto>> GetPendingBookingsAsync(int? skip = null, int? take = null)
        {
            var bookings = await _bookingRepository.GetPendingBookingsAsync(skip, take);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> UpdateBookingAsync(Guid id, UpdateBookingDto request)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
                return null;

            booking.Status = request.Status ?? booking.Status;
            booking.Confirmed = request.Confirmed;
            booking.Canceled = request.Canceled;

            // Fix for CS0019: Use a conditional operator to handle the nullable decimal type
            booking.TotalPrice = request.TotalPrice != null ? request.TotalPrice : booking.TotalPrice;

            booking.Notes = request.Notes ?? booking.Notes;
            booking.UpdatedAt = DateTime.UtcNow;

            var updatedBooking = await _bookingRepository.UpdateAsync(booking);
            return _mapper.Map<BookingDto>(updatedBooking);
        }

        public async Task<bool> UpdateBookingStatusAsync(Guid bookingId, string status)
        {
            return await _bookingRepository.UpdateBookingStatusAsync(bookingId, status);
        }
    }
}
