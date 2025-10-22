using System.ComponentModel.DataAnnotations;

namespace AgAmuasiBooking.Controllers.Models;

/// <summary>
/// DTO for service cost information.
/// </summary>
public record ServiceCostDto(
    int ID,
    [Range(1, double.MaxValue)] decimal Cost
);
