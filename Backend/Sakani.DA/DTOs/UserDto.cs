using System;
using Sakani.Data.Models;

namespace Sakani.DA.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
} 