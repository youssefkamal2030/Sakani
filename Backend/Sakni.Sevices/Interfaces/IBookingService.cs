using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.DA.DTOs;

namespace Sakni.Sevices.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> GetBookingByIdAsync(Guid id);
        Task<IEnumerable<BookingDto>> GetBookingsByStudentIdAsync(Guid studentId, int? skip = null, int? take = null);
        Task<IEnumerable<BookingDto>> GetBookingsByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null);
        Task<BookingDto> CreateBookingAsync(CreateBookingDto request);
        Task<BookingDto> UpdateBookingAsync(Guid id, UpdateBookingDto request);
        Task<bool> DeleteBookingAsync(Guid id);
        Task<BookingDto> GetBookingWithDetailsAsync(Guid bookingId);
        Task<bool> UpdateBookingStatusAsync(Guid bookingId, string status);
        Task<bool> CancelBookingAsync(Guid bookingId);
        Task<bool> ConfirmBookingAsync(Guid bookingId);
        Task<int> GetTotalBookingsCountAsync();
        Task<IEnumerable<BookingDto>> GetPendingBookingsAsync(int? skip = null, int? take = null);
        Task<IEnumerable<BookingDto>> GetConfirmedBookingsAsync(int? skip = null, int? take = null);
        Task<IEnumerable<BookingDto>> GetCanceledBookingsAsync(int? skip = null, int? take = null);
        Task<decimal> GetTotalBookingsAmountAsync(DateTime startDate, DateTime endDate);
    }
}
