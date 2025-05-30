using System;
using System.Collections.Generic;

namespace Sakani.DA.DTOs
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public Guid ApartmentId { get; set; }
        public string RoomType { get; set; }
        public int NumberOfBeds { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<BedDto> Beds { get; set; }
    }

    public class CreateRoomDto
    {
        public Guid ApartmentId { get; set; }
        public string RoomType { get; set; }
        public int NumberOfBeds { get; set; }
    }

    public class UpdateRoomDto
    {
        public string RoomType { get; set; }
        public int NumberOfBeds { get; set; }
    }
} 