using AgAmuasiBooking.Context;
using System.ComponentModel.DataAnnotations;

namespace AgAmuasiBooking.Models.AuthVm
{
    public class RegisterVM
    {
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public required string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public required string Title { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6)]
        public required string Password { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public required string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public required string FullName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10)]
        public required string PhoneNumber { get; set; }

        internal ApplicationUser Transform => new() { Email = Email, Password = Password, PhoneNumber = PhoneNumber, FullName = FullName, UserName = Email, ConfirmPassword = ConfirmPassword };
    }
}
