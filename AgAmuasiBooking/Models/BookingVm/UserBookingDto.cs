namespace AgAmuasiBooking.Models.BookingVm;

public record UserBookingDto(Guid bookingsid, DateTime BookingDate, string Title, string Purpose, Array Dates, short Guests, bool IsReviewed, bool IsApproved, bool HasPaid, short Days, int BookingServicesID, int ServiceCostsID, decimal Cost);