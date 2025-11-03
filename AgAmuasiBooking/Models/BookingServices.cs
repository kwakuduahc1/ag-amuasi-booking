using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgAmuasiBooking.Models
{
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
