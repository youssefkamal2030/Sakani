using System;

namespace Sakani.DA.DTOs
{
    public class OwnerDto
    {
        public Guid OwnerId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Residence { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePhoto { get; set; }
        public string VerificationStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateOwnerDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Residence { get; set; }
        public string? Gender { get; set; }
        public string? Religion { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class UpdateOwnerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Residence { get; set; }
        public string Religion { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePhoto { get; set; }
    }
} 