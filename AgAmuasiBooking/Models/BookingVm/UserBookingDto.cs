namespace AgAmuasiBooking.Models.BookingVm;

public record UserBookingDto(Guid BookingsID, DateTime BookingDate, string Title, string Purpose, Array Dates, short Guests, bool IsReviewed, bool IsApproved, bool HasPaid, short Days, int BookingServicesID, int ServiceCostsID, decimal Cost);