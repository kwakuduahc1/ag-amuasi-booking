using AgAmuasiBooking.Context;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgAmuasiBooking.Models
{

    [Table("Bookings")]
    [Index("UserID", ["Title"])]
    public class Bookings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public Guid BookingsID { get; set; } = Guid.CreateVersion7(DateTimeOffset.UtcNow);

        [Required]
        public required Guid UserID { get; set; }

        [Required]
        public required DateTime BookingDate { get; set; } = DateTime.UtcNow;

        [StringLength(200, MinimumLength = 5)]
        public string? Purpose { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public required string Title { get; set; }

        [Required]
        public required DateTime[] Dates { get; set; }

        [Required]
        [Range(1, 18)]
        public required short Guests { get; set; } = 1;

        [DefaultValue(false)]
        public bool IsReviewed { get; set; } = false;

        [DefaultValue(false)]
        public bool IsApproved { get; set; } = false;

        [Range(0, double.MaxValue)]
        public decimal AmountPaid { get; set; } = 0;

        [DefaultValue(false)]
        public bool HasPaid { get; set; } = false;

        [DefaultValue(false)]
        public bool Deleted { get; set; } = false;

        public DateTime? ReviewedDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string Reviewer { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public virtual ICollection<BookingServices>? BookingServices { get; set; }
    }

    [Index(nameof(ServiceName), IsUnique = true)]
    public class Services
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int ServicesID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string ServiceName { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool PerPerson { get; set; }

        public virtual ICollection<ServiceCosts>? ServiceCosts { get; set; }

    }

    public class ServiceCosts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int ServiceCostsID { get; set; }
       
        [Required]
        public int ServicesID { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public required decimal Cost { get; set; }

        [Required]
        public required DateTime DateSet { get; set; } = DateTime.UtcNow;

        public virtual Services? Service { get; set; }
    }

    public class BookingServices
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int BookingServicesID { get; set; }

        [Required]
        public required Guid BookingsID { get; set; }

        [Required]
        public required int ServiceCostsID { get; set; }

        public virtual Bookings? Booking { get; set; }
        public virtual ServiceCosts? ServiceCosts { get; set; }
    }
}
