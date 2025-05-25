using System;

namespace Sakani.DA.DTOs
{
    public class BedDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Guid? StudentId { get; set; }
        public decimal Price { get; set; }
        public bool IsAC { get; set; }
        public bool IsVacant { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateBedDto
    {
        public decimal Price { get; set; }
        public bool IsAC { get; set; }
    }

    public class UpdateBedDto
    {
        public decimal Price { get; set; }
        public bool IsAC { get; set; }
        public bool IsVacant { get; set; }
    }
} 