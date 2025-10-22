namespace AgAmuasiBooking.Controllers.Models;

/// <summary>
/// DTO for service data with associated cost information.
/// Used in service listing queries.
/// </summary>
public record ServicesDto(
    int ServicesID,
    string ServiceName,
    decimal Cost,
    int ServiceCostsID
);
