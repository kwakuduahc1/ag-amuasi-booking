using System.ComponentModel.DataAnnotations;

namespace AgAmuasiBooking.Controllers.Models;

/// <summary>
/// DTO for adding or updating a service.
/// If ServicesID > 0, updates existing service name.
/// If ServicesID = 0, creates new service with initial cost.
/// </summary>
public record AddServiceDto(
    int ServicesID,
    [StringLength(50, MinimumLength = 3)] string ServiceName,
    [Range(1, double.MaxValue)] decimal Cost,
    bool PerPerson
);
