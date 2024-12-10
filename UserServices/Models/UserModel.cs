namespace CareerMatch.UserServices.Models;

using System;
using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MinLength(8)]
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastLogin { get; set; }

        // Role-based authorization
        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "User"; // Default role

        // GDPR Compliance: Soft delete for user data
        public bool IsDeleted { get; set; } = false;

        // Employer-specific data
        public bool IsEmployer { get; set; } = false;

        // Optional: GDPR compliance, stores consent timestamps
        public DateTime? TermsAcceptedAt { get; set; }

        public DateTime? PrivacyPolicyAcceptedAt { get; set; }
    }
