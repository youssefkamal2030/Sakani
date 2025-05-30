using System;
using System.Collections.Generic;

namespace Sakani.DA.DTOs
{
    public class ApartmentDto
    {
        public Guid ApartmentId { get; set; }
        public string ApartmentTitle { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBeds { get; set; }
        public bool? IsWifi { get; set; }
        public Guid? OwnerId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string MainImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<RoomDto> Rooms { get; set; }
    }

    public class CreateApartmentDto
    {
        public string? ApartmentTitle { get; set; }
        public int? NumberOfRooms { get; set; }
        public int? NumberOfBeds { get; set; }
        public bool? IsWifi { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? MainImage { get; set; }
        public Guid OwnerId { get; set; }
    }

    public class UpdateApartmentDto
    {
       public string? ApartmentTitle { get; set; }
        public int? NumberOfRooms { get; set; }
        public int? NumberOfBeds { get; set; }
        public bool? IsWifi { get; set; }
        public Guid? OwnerId { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? MainImage { get; set; }
    }
} 