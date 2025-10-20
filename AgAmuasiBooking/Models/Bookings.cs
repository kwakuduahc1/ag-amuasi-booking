using AgAmuasiBooking.Context;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgAmuasiBooking.Models
{

    [Index(nameof(UserName), [nameof(Title)])]
    public class Bookings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public Guid BookingsID { get; set; } = Guid.CreateVersion7(DateTimeOffset.UtcNow);

        [Required]
        [StringLength(70, MinimumLength = 10)]
        public required string UserName { get; set; }

        [Required]
        public required DateTime BookingDate { get; set; } = DateTime.UtcNow;

        [StringLength(200, MinimumLength = 5)]
        public string? Purpose { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public required string Title { get; set; }

        [Required]
        public required DateTime[] Dates { get; set; }

        [Required]
        public required short Days { get; set; }

        [Required]
        [Range(1, 18)]
        public required short Guests { get; set; } = 1;

        [DefaultValue(false)]
        public bool IsReviewed { get; set; } = false;

        [DefaultValue(false)]
        public bool IsApproved { get; set; } = false;

        public DateTime? DateApproved;

        [StringLength(70, MinimumLength = 10)]
        public string? Approver { get; set; }

        [Range(0, double.MaxValue)]
        public decimal AmountPaid { get; set; } = 0;

        [DefaultValue(false)]
        public bool HasPaid { get; set; } = false;

        [DefaultValue(false)]
        public bool Deleted { get; set; } = false;

        public DateTime? ReviewedDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        [StringLength(70, MinimumLength = 10)]
        public string? Receiver { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string Reviewer { get; set; }

        [DefaultValue(false)]
        public bool IsClosed { get; set; }

        [DefaultValue(false)]
        public bool IsCancelled { get; set; }

        public DateTime? DateCancelled { get; set; }

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

    [Index(nameof(BookingsID), [nameof(ServiceCostsID)])]
    [Index(nameof(ServiceCostsID), [nameof(BookingsID)])]
    public class BookingServices
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int BookingServicesID { get; set; }

        [Required]
        public Guid BookingsID { get; set; }

        [Required]
        public required int ServiceCostsID { get; set; }

        public virtual Bookings? Booking { get; set; }
        public virtual ServiceCosts? ServiceCosts { get; set; }
    }
}
