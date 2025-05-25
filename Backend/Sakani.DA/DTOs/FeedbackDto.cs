using System;

namespace Sakani.DA.DTOs
{
    public class FeedbackDto
    {
        public Guid FeedbackId { get; set; }
        public string UserId { get; set; }
        public Guid ApartmentId { get; set; }
        public int? Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateFeedbackDto
    {
        public Guid ApartmentId { get; set; }
        public int? Rate { get; set; }
        public string Comment { get; set; }
    }

    public class UpdateFeedbackDto
    {
        public int? Rate { get; set; }
        public string Comment { get; set; }
    }
} 