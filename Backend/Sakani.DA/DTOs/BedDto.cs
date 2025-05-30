using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Sakani.DA.DTOs
{
    public class BedDto
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Guid? StudentId { get; set; }
        public decimal Price { get; set; }
        public bool IsAC { get; set; }
        public bool IsVacant { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateBedDto
    {
        [Required]
        public Guid RoomId { get; set; }
        [AllowNull]
        public Guid StudentId { get; set; }
        [AllowNull]
        public decimal Price { get; set; }
        [AllowNull]
        public bool IsVacant { get; set; }
    }

    public class UpdateBedDto
    {
        [AllowNull]
        public decimal Price { get; set; }
        [AllowNull]
        public bool IsAC { get; set; }
        [AllowNull]
        public bool IsVacant { get; set; }
    }
} 