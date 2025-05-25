using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsByStudentIdAsync(Guid studentId, int? skip = null, int? take = null);
        Task<IEnumerable<Booking>> GetBookingsByApartmentIdAsync(Guid apartmentId, int? skip = null, int? take = null);
        Task<Booking> GetBookingWithDetailsAsync(Guid bookingId);
        Task<bool> UpdateBookingStatusAsync(Guid bookingId, string status);
        Task<bool> CancelBookingAsync(Guid bookingId);
        Task<bool> ConfirmBookingAsync(Guid bookingId);
        Task<int> GetTotalBookingsCountAsync();
        Task<IEnumerable<Booking>> GetPendingBookingsAsync(int? skip = null, int? take = null);
        Task<IEnumerable<Booking>> GetConfirmedBookingsAsync(int? skip = null, int? take = null);
        Task<IEnumerable<Booking>> GetCanceledBookingsAsync(int? skip = null, int? take = null);
        Task<decimal> GetTotalBookingsAmountAsync(DateTime startDate, DateTime endDate);
    }
} 