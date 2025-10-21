namespace AgAmuasiBooking.Models.BookingVm;

/// <summary>
/// Record representing the result of the fn_user_bookings PostgreSQL function.
/// Maps to the user's booking data with associated service costs.
/// </summary>
public record UserBookingDto(
    Guid BookingsId,           // UUID from bookingsid
    DateTime BookingDate,       // TIMESTAMP WITH TIME ZONE
    string Title,              // VARCHAR(50)
    string? Purpose,           // VARCHAR(200) - nullable
    DateTime[] Dates,          // TIMESTAMP WITH TIME ZONE[]
    short Guests,              // smallint
    bool IsReviewed,           // boolean
    bool IsApproved,           // boolean
    bool HasPaid,              // boolean
    short Days,                // smallint
    int? BookingServicesId,    // int - nullable (LEFT JOIN)
    int? ServiceCostsId,       // int - nullable (LEFT JOIN)
    decimal? Cost              // numeric - nullable (LEFT JOIN)
);
