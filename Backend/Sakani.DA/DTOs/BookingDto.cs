using System;

namespace Sakani.DA.DTOs
{
    public class BookingDto
    {
        public Guid BookingId { get; set; }
        public Guid StudentId { get; set; }
        public Guid ApartmentId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public bool Confirmed { get; set; }
        public bool Canceled { get; set; }
        public decimal? TotalPrice { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateBookingDto
    {
        public Guid ApartmentId { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateBookingDto
    {
        public string Status { get; set; }
        public bool Confirmed { get; set; }
        public bool Canceled { get; set; }
        public decimal? TotalPrice { get; set; }
        public string Notes { get; set; }
    }
} 