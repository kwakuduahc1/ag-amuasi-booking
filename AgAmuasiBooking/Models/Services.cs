using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgAmuasiBooking.Models
{
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
}
