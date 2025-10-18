﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgAmuasiBooking.Context
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public override string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6)]
        [NotMapped]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        [NotMapped]
        public required string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10)]
        public override string? PhoneNumber { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 6)]
        public required string FullName { get; set; }
    }

    public record SetRoleDto([Required, StringLength(64, MinimumLength = 32)] string UserId, [Required, StringLength(20, MinimumLength = 10)] string Role);
}
