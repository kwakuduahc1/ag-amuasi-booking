using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgAmuasiBooking.Models
{
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
}
