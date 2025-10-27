using AgAmuasiBooking.Context;
using AgAmuasiBooking.Models;
using AgAmuasiBooking.Models.BookingVm;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AgAmuasiBooking.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [EnableCors("bStudioApps")]
    [Authorize(Policy = "Administration")]
    public class MyBookingsController(DbContextOptions<ApplicationDbContext> dbContextOptions, CancellationToken token) : ControllerBase
    {
        private readonly ApplicationDbContext db = new(dbContextOptions);

        [HttpGet("{num:min(1):max(20)}")]
        public async Task<IActionResult> MyBookings(byte num = 5)
        {
            var user = User.Identity?.Name;
            if (string.IsNullOrEmpty(user))
                return Unauthorized(new { Message = "Invalid credentials" });

            using var con = db.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync(token);

            const string qry =
                """
                SELECT bookingsid, bookingdate, title, purpose, dates, guests, isreviewed, isapproved, haspaid, days, bookingservicesid, servicecostsid, "cost"
                FROM fn_user_bookings(@user, @num)
                """;
            var bookings = await con.QueryAsync<UserBookingDto>(qry, new { user, num });

            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> Booking([FromBody] AddBookingDto booking)
        {
            var book = new Bookings
            {
                BookingDate = DateTime.UtcNow,
                Dates = booking.Dates,
                Days = (short)booking.Dates.Length,
                Guests = booking.Guests,
                Reviewer = "",
                Title = booking.Title,
                UserName = User.Identity.Name,
                Purpose = booking.Purpose,
                IsReviewed = false,
                HasPaid = false,
                AmountPaid = 0,
                Deleted = false,
                IsApproved = false,
                PaymentDate = null,
                ReviewedDate = null,
                BookingServices = []
            };
            for (int i = 0; i < booking.Services.Length; i++)
                book.BookingServices.Add(new BookingServices
                {
                    ServiceCostsID = i,
                    BookingsID = book.BookingsID
                });
            db.Bookings.Add(book);
            await db.SaveChangesAsync(token);
            return Ok(book.BookingsID);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditBookingDto dto)
        {
            var bk = await db.Bookings.FindAsync(dto.BookingsID, token);
            if (bk is null)
                return NotFound(new { Message = "Booking not found" });
            db.Entry(bk).State = EntityState.Deleted;
            var book = new Bookings
            {
                BookingDate = DateTime.UtcNow,
                Dates = dto.Dates,
                Days = (short)dto.Dates.Length,
                Guests = dto.Guests,
                Reviewer = "",
                Title = dto.Title,
                UserName = User.Identity.Name,
                Purpose = dto.Purpose,
                IsReviewed = false,
                HasPaid = false,
                AmountPaid = 0,
                Deleted = false,
                IsApproved = false,
                PaymentDate = null,
                ReviewedDate = null,
                BookingServices = []
            };
            for (int i = 0; i < dto.Services.Length; i++)
                book.BookingServices.Add(new BookingServices
                {
                    ServiceCostsID = i,
                    BookingsID = book.BookingsID
                });
            db.Bookings.Add(book);
            await db.SaveChangesAsync(token);
            return Ok(book.BookingsID);
        }

        [HttpDelete("{id:required:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var booking = await db.Bookings.FindAsync(id, token);
            if (booking is null)
                return BadRequest(new { Message = "Booking not found" });
            db.Entry(booking).State = EntityState.Deleted;
            await db.SaveChangesAsync(token);
            return Ok();
        }
    }
}