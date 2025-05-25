using System;

namespace Sakani.DA.DTOs
{
    public class StudentDto
    {
        public Guid StudentId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string CollegeName { get; set; }
        public int Age { get; set; }
        public string Origin { get; set; }
        public string ProfilePhoto { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateStudentDto
    {
        public string Name { get; set; }
        public string CollegeName { get; set; }
        public int Age { get; set; }
        public string Origin { get; set; }
        public string Gender { get; set; }
    }

    public class UpdateStudentDto
    {
        public string Name { get; set; }
        public string CollegeName { get; set; }
        public int Age { get; set; }
        public string Origin { get; set; }
        public string ProfilePhoto { get; set; }
    }
} 